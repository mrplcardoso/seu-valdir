using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerObject : BaseObject
{
	public PlayerController playerController;
	public PlayerAnimation playerAnimation;
	public PlayerCamera playerCamera;
	public PlayerEventTrigger playerEvent;

	public bool lockCamera;

	private void Start()
	{
		playerController = GetComponent<PlayerController>();
		playerAnimation = GetComponent<PlayerAnimation>();
		playerEvent = GetComponent<PlayerEventTrigger>();
		SetCamera();
	}

	void SetCamera()
	{
		playerCamera = Camera.main.GetComponent<PlayerCamera>();
		playerCamera.playerBody = transform;
		playerCamera.playerHead = GetComponentsInChildren<Transform>()[1];
	}

	public override void FrameUpdate()
	{
		playerController.ReadInputs();
		playerController.Move();
		if (!lockCamera)
		{ playerCamera.RotateAround(); }
		playerEvent.OverLapEventTrigger();
	}

	public override void PhysicsUpdate()
	{
		
	}

	public override void PostUpdate()
	{

	}
}
