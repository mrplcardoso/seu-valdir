using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossStreetMachine : StateMachine
{
	public PlayerObjectNoCamera player;

	private void Start()
	{
		StartCoroutine(StartState());
	}

	IEnumerator StartState()
	{
		yield return null;
		ChangeState<TutorialCrossStreetState>();
	}
}
