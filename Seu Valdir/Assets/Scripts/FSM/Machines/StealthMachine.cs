using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthMachine : StateMachine
{
	private void Start()
	{
		ChangeState<TutorialStealthState>();
	}
}
