using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
	public PlayerObject playerObject;
	CharacterController controller;
	PlayerAnimation playerAnimator { get { return playerObject.playerAnimation; } }
	private Vector3 strafe, forward, vertical;
	private float gravity, jumpSpeed;
	public float jumpHeight;
	public float jumpHeightTime;
	public AudioClip moveAudio;
	public AudioClip chutadoAudio;
	public AudioSource audioSource;

	private void Start()
	{
		playerObject = GetComponent<PlayerObject>();
		controller = GetComponent<CharacterController>();
		audioSource = GetComponent<AudioSource>();
		if (moveAudio != null)
		{
			audioSource.clip = moveAudio;
		}
		/*gravity = (-2 * jumpHeight) / (jumpHeightTime * jumpHeightTime);
		jumpSpeed = (-2 * jumpHeight) / jumpHeightTime;*/
	}

	public void ReadInputs()
	{
		//strafe = Input.GetAxis("Horizontal") * speed * transform.right;
		forward = Input.GetAxis("Vertical") * speed * transform.forward;

		/*vertical += gravity * Time.deltaTime * Vector3.up;

		if(controller.isGrounded)
		{ vertical = Vector3.down; }

		if(Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
		{	vertical = jumpSpeed * Vector3.up;	}*/

		velocity = forward; //+ strafe + vertical;
		playerAnimator.andandoAnim = velocity.normalized.sqrMagnitude;
	}

	public override void Move()
	{
		if(!audioSource.isPlaying)
		{
			if(moveAudio != null)
			{
				if((int)velocity.magnitude == (int)speed)
				{
					audioSource.Play();
				}
			}
		}
		//else { audioSource.Stop(); }
		controller.Move(velocity * Time.deltaTime);
	}
}
