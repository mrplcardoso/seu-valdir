using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsMenuState : MainMenuState
{
	private void Start()
	{
		menuView.color = Color.clear;
		buttons = menuView.GetComponentsInChildren<Button>(true);

		/*Back*/
		Button.ButtonClickedEvent click = new Button.ButtonClickedEvent();
		click.AddListener(delegate ()
		{
			StartCoroutine(ExitMenu());
		});
		buttons[0].onClick = click;
	}

	public override void OnEnter()
	{
		StartCoroutine(EnterMenu());
	}

	IEnumerator EnterMenu()
	{
		yield return new WaitForSeconds(1f);
		menuView.gameObject.SetActive(true);
		yield return FlipIn();

		/*Back*/
		buttons[0].interactable = true;
	}

	IEnumerator ExitMenu()
	{
		/*Back*/
		buttons[0].interactable = false;

		//yield return new WaitForSeconds(1f);
		yield return FlipOut();
		stateMachine.ChangeState<TitleMenuState>();
		menuView.gameObject.SetActive(false);
	}
}
