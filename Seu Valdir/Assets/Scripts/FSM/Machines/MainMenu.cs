using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : StateMachine
{
	private void Start()
	{
		ChangeState<InitializeMenuState>();
	}
}
