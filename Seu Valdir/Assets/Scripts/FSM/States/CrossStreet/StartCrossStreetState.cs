using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartCrossStreetState : CrossStreetState
{
	Text startText;

	private void Start()
	{
		startText = tutorialBackground.GetComponentInChildren<Text>();
	}

	public override void OnEnter()
	{
		startText.rectTransform.localPosition = Vector3.zero;
		startText.fontSize = 120;
		StartCoroutine(CountDown());
	}

	IEnumerator CountDown()
	{
		int count = 0;
		while (count > -1)
		{
			if (count == 0)
			{ startText.text = "Start"; }
			else { startText.text = count.ToString(); }

			yield return FlipIn(null, startText);
			yield return new WaitForSeconds(0.5f);

			yield return FlipOut(2f, null, startText);
			startText.text = "";
			count--;
			yield return new WaitForSeconds(0.5f);
		}
		crossStreetMachine.ChangeState<UpdateCrossStreetState>();
	}
}
