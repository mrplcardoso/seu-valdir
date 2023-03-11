using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStealthController : BaseController
{
	PlayerAnimation anim;
	CharacterController controller;
	public AudioSource audioSource;
	public Vector3 direction { get; private set; }
	public bool caught;
	public bool endMiniGame;

	private void Start()
	{
		controller = GetComponent<CharacterController>();
		anim = GetComponent<PlayerAnimation>();
		audioSource = GetComponent<AudioSource>();
		speed = 5;
	}

	public override void Move()
	{
		direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;

		if (direction.sqrMagnitude != 0)
		{ transform.rotation = Quaternion.LookRotation(direction); }

		velocity = direction * speed * Time.deltaTime;
		anim.andandoAnim = velocity.magnitude;
		if(!audioSource.isPlaying)
		{
			if(velocity.magnitude > 0)
			{audioSource.Play(); }
		}
		controller.Move(velocity);
	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("StealthDestination"))
		{ endMiniGame = true; }
	}
}
