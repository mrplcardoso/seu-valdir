using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaldirTestController : MonoBehaviour
{
	enum AnimState { Parado = 0, Andando, Esbarrado, Atropelado }
	[SerializeField]
	AnimState state;

	CharacterController controller;
	Animator animator;
	Vector3 direction;
	float speed;

	private void Start()
	{
		controller = GetComponent<CharacterController>();
		animator = GetComponentInChildren<Animator>();
		speed = 5f;
	}

	private void Update()
	{
		switch(state)
		{
			case AnimState.Andando:
			{
				animator.SetFloat("Andando", 1.0f);
				return;
			}
			case AnimState.Esbarrado:
			{
				animator.SetTrigger("Esbarrado");
				state = AnimState.Parado;
				return;
			}
			case AnimState.Atropelado:
			{
				animator.SetTrigger("Atropelado");
				state = AnimState.Parado;
				return;
			}
			default:
			{
				animator.SetFloat("Andando", 0.0f);
				return; 
			}
		}
	}


	/*
	private void Update()
	{
		direction.x = Input.GetAxis("Horizontal");
		direction.z = Input.GetAxis("Vertical");
		//controller.Move(direction.normalized * speed * Time.deltaTime);
	}*/
}
