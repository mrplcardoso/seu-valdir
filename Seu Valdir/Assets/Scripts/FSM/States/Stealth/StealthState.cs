using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class StealthState : State
{
	protected StealthMachine stealthMachine
	{ get { return (StealthMachine)stateMachine; } }

	public Image tutorialBackground;

	protected virtual IEnumerator FlipIn(Image img = null, Text txt = null)
	{
		Color c = tutorialBackground.color;
		if (img != null) { img.color = c; }
		if (txt != null) { txt.color = c; }
		Color end = Color.black;
		Color endText = Color.yellow;
		Color endImg = Color.white;
		float t = 0;
		while (t < 1.01f)
		{
			tutorialBackground.color = Color.Lerp(c, end, t);
			if (img != null) { img.color = Color.Lerp(c, endImg, t); }
			if (txt != null) { txt.color = Color.Lerp(c, endText, t); }
			t += Time.deltaTime;
			yield return null;
		}
		tutorialBackground.color = end;
		if (img != null) { img.color = endImg; }
		if (txt != null) { txt.color = endText; }
	}

	protected virtual IEnumerator FlipOut(float incTime = 1f, Image img = null, Text txt = null)
	{
		Color c = tutorialBackground.color;
		Color cImg = (img != null) ? img.color : Color.clear;
		Color cTxt = (txt != null) ? txt.color : Color.clear;
		Color end = Color.clear;

		float t = 0;
		while (t < 1.01f)
		{
			tutorialBackground.color = Color.Lerp(c, end, t);
			if (img != null) { img.color = Color.Lerp(cImg, end, t); }
			if (txt != null) { txt.color = Color.Lerp(cTxt, end, t); }
			t += incTime * Time.deltaTime;
			yield return null;
		}
		tutorialBackground.color = end;
		if (img != null) { img.color = end; }
		if (txt != null) { txt.color = end; }
	}
}
