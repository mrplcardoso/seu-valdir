using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UpdateStealthState : StealthState
{
	public PlayerStealthController player;
	public List<NPCGuard> guards;
	public bool loopElements;

	private void Start()
	{
		loopElements = false;
	}

	public override void OnEnter()
	{
		for (int i = 0; i < guards.Count; ++i)
		{ guards[i].Init(player); }
		loopElements = true;
	}

	private void Update()
	{
		if (!loopElements) return;

		player.Move();
		for (int i = 0; i < guards.Count; ++i)
		{ guards[i].FrameUpdate(); }

		if(player.caught)
		{ stealthMachine.ChangeState<ReStartStealthState>(); }

		if(player.endMiniGame)
		{ 
			loopElements = false;
			SceneManager.LoadScene("EndGame");
		}
	}

	public override void OnExit()
	{
		loopElements = false;
	}
}
