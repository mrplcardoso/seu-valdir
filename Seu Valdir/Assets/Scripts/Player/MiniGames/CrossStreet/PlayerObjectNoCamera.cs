using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObjectNoCamera : BaseObject
{
	public PlayerController playerController;
	public PlayerAnimation playerAnimation;
	public AudioSource playerAudio;
	public AudioClip collideAudio;
	public float tempSpeed;

	public bool finished;
	bool playMoveAudio;
	bool canCollide;

	private void Start()
	{
		playMoveAudio = true;
		canCollide = true;
		playerController = GetComponent<PlayerController>();
		tempSpeed = playerController.speed;
		playerAnimation = GetComponent<PlayerAnimation>();
		//playerAudio = GetComponent<AudioSource>();
	}

	public override void FrameUpdate()
	{
		SetVelocity();
		playerAnimation.andandoAnim = playerController.velocity.magnitude;
		playerController.Move();
	}

	void SetVelocity()
	{
		playerController.velocity =
			new Vector3(Input.GetAxis("Vertical"), 0, -Input.GetAxis("Horizontal"));
		playerController.velocity = playerController.velocity.normalized * playerController.speed;
		if(playerController.velocity.sqrMagnitude > 0 && playMoveAudio) 
		{
			//StartCoroutine(PlayMoveAudio());
		}
	}
	IEnumerator PlayMoveAudio()
	{
		playMoveAudio = false;
		if (canCollide)
		{ PlayAudio(playerController.moveAudio); }
		yield return new WaitForSeconds(1.2f);
		playMoveAudio = true;
	}

	public override void PhysicsUpdate()
	{

	}

	public override void PostUpdate()
	{

	}

	private void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("NPC") && canCollide)
		{
			playerAnimation.setTriggerAnim = "Esbarrado";
			PlayAudio(collideAudio);
			//playerController.speed = 0;
			StartCoroutine(OnAnimationComplete());
		}
		if(other.gameObject.CompareTag("EventTrigger"))
		{ finished = true; }
	}

	IEnumerator OnAnimationComplete()
	{
		canCollide = false;
		float t = 1f;
		while (t > 0f)
		{
			playerController.velocity = transform.forward * -(playerController.speed * 1.25f);
			playerController.Move();
			t -= 0.85f * Time.deltaTime;
			yield return null; 
		}
		yield return new WaitForSeconds(0.5f);
		canCollide = true;
		//playerController.speed = tempSpeed;
	}

	void PlayAudio(AudioClip audio)
	{
		playerAudio.clip = audio;
		if (!playerAudio.isPlaying)
		{ playerAudio.Play(); }
	}
}
