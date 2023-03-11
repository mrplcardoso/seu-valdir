using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateBrokenMirrorState : BrokenMirrorState
{
	public BrokenMirrorPlayerController player;

	bool loopUpdate;

	private void Start()
	{
		loopUpdate = false;
	}

	public override void OnEnter()
	{
		loopUpdate = true;
	}

	private void Update()
	{
		if (!loopUpdate) return;

		if(player.BrokeAllMirrors())
		{ brokenMirrorMachine.ChangeState<EndBrokenMirrorState>(); }

		player.FrameUpdate();
	}

	public override void OnExit()
	{
		loopUpdate = false;
	}
}
