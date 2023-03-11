using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeMenuState : MainMenuState
{
	public override void OnEnter()
	{
		GameCrossInformation.currentEvent = 0;
		Cursor.lockState = CursorLockMode.Confined;
		StartCoroutine(MoveNext());
	}

	IEnumerator MoveNext()
	{
		yield return null;
		stateMachine.ChangeState<TitleMenuState>();
	}
}
