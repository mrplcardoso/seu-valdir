using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateGameState : GameState
{
	public bool loopElements;

	public void Start()
	{
		loopElements = false;
	}

	public override void OnEnter()
	{
		loopElements = true;
	}

	public override void OnExit()
	{
		loopElements = false;
		gameMachine.player.playerAnimation.andandoAnim = 0;
	}

	public void Update()
	{
		if (!loopElements) return;

		gameMachine.ElementsUpdate();
		gameMachine.CheckForEvent();
	}

	public void FixedUpdate()
	{
		if (!loopElements) return;

		gameMachine.ElementsPhysicsUpdate();
	}

	public void LateUpdate()
	{
		if (!loopElements) return;

		gameMachine.ElementsPostUpdate();
	}
}
