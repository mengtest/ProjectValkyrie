﻿using UnityEngine;
using System.Collections;

public class EnemyBattleManager : MonoBehaviour {

	private Animator characterAnimator;
	private CharacterController controller;
	public float speed = 0.5f;
	public float runSpeed = 1.7f;
	public float gravity = 20.0f;
	private Vector3 moveDirection = Vector3.zero;

	private DistanceSearch distanceScripts;

	private AnimatorStateInfo stateInfo;

	void Start () 
    {
		distanceScripts = GetComponent<DistanceSearch> ();
		characterAnimator = GetComponent<Animator>();
		controller = GetComponent<CharacterController> ();
		characterAnimator.SetInteger ("moving", 1);
	}

	void Update ()
	{
		stateInfo = characterAnimator.GetCurrentAnimatorStateInfo(0);

		if (distanceScripts.SearchedFlag) {
			characterAnimator.SetInteger ("moving", 0);
			characterAnimator.SetInteger ("battle", 1);
            //idle or run -> battle change
		} 
        else
        {
			characterAnimator.SetInteger ("battle", 0);
			characterAnimator.SetInteger ("moving", 1);
            //battle -> run or idle chage
		}

		if (stateInfo.IsName("idle_battle")) 
        {
			characterAnimator.SetInteger ("moving", Random.Range(3, 5));//AttackMotion Random Play
		}

		if (characterAnimator.GetInteger ("moving") == 1) {
			if(controller.isGrounded)
			{
				moveDirection = transform.forward * speed * runSpeed;

			}
			controller.Move(moveDirection * Time.deltaTime);
			moveDirection.y -= gravity * Time.deltaTime;
		}
	}
}