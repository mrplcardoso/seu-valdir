using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrombadinhaController : BaseController
{
	CharacterController controller;
	public AudioSource audioSource;

	private void Start()
	{
		controller = GetComponent<CharacterController>();
		audioSource = GetComponent<AudioSource>();
	}

	public override void Move()
	{
		//controller.Move(velocity * speed * Time.deltaTime);
		transform.position += (velocity * speed * Time.deltaTime);
	}
}
