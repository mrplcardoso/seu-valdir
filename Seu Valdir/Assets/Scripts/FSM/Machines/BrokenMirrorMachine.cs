using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenMirrorMachine : StateMachine
{
	private void Start()
	{
		StartCoroutine(StartState());
	}

	IEnumerator StartState()
	{
		yield return null;
		ChangeState<TutorialBrokenMirrorState>();
		//ChangeState<UpdateBrokenMirrorState>();
	}
}