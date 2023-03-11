using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleMenuState : MainMenuState
{
	private void Start()
	{
		menuView.color = Color.clear;
		buttons = menuView.GetComponentsInChildren<Button>(true);

		/*Start*/
		Button.ButtonClickedEvent click = new Button.ButtonClickedEvent();
		click.AddListener(delegate ()
		{
			StartCoroutine(ChangeScene("Game"));
		});
		buttons[0].onClick = click;

		/*Credits*/
		click = new Button.ButtonClickedEvent();
		click.AddListener(delegate ()
		{
			StartCoroutine(ExitMenu());
		});
		buttons[1].onClick = click;
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

		/*Start*/
		buttons[0].interactable = true;

		/*Credits*/
		buttons[1].interactable = true;
	}

	IEnumerator ExitMenu()
	{
		/*Start*/
		buttons[0].interactable = false;

		/*Credits*/
		buttons[1].interactable = false;

		//yield return new WaitForSeconds(1f);
		yield return FlipOut();
		stateMachine.ChangeState<CreditsMenuState>();
		menuView.gameObject.SetActive(false);
	}

	protected IEnumerator ChangeScene(string name)
	{
		/*Start*/
		buttons[0].interactable = false;

		/*Credits*/
		buttons[1].interactable = false;

		Cursor.lockState = CursorLockMode.Locked;
		yield return FlipOut();
		SceneManager.LoadScene(name);
	}
}
