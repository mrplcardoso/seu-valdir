using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class MainMenuState : State
{
	public Image menuView;
	protected Button[] buttons;

	protected IEnumerator FlipIn()
	{
		Color c = menuView.color;
		Color end = Color.white;
		Image[] btn = new Image[buttons.Length];
		for (int i = 0; i < buttons.Length; ++i)
		{
			btn[i] = buttons[i].GetComponent<Image>();
			btn[i].color = c;
		}

		float t = 0;
		while (t < 1.01f)
		{
			menuView.color = Color.Lerp(c, end, t);
			for (int i = 0; i < buttons.Length; ++i)
			{
				btn[i].color = Color.Lerp(c, end, t);
			}
			t += Time.deltaTime;
			yield return null;
		}
		menuView.color = end;
		for (int i = 0; i < buttons.Length; ++i)
		{
			btn[i].color = end;
		}
	}

	protected IEnumerator FlipOut()
	{
		Color c = menuView.color;
		Color end = Color.clear;
		Image[] btn = new Image[buttons.Length];
		for (int i = 0; i < buttons.Length; ++i)
		{
			btn[i] = buttons[i].GetComponent<Image>();
			btn[i].color = c;
		}

		float t = 0;
		while (t < 1.01f)
		{
			menuView.color = Color.Lerp(c, end, t);
			for (int i = 0; i < buttons.Length; ++i)
			{
				btn[i].color = Color.Lerp(c, end, t);
			}

			t += Time.deltaTime;
			yield return null;
		}
		menuView.color = end;
		for (int i = 0; i < buttons.Length; ++i)
		{
			btn[i].color = end;
		}
	}

	/*
	protected IEnumerator FlipIn()
	{
		float startAng = menuView.rectTransform.eulerAngles.y;
		float t = 0;
		while (t < 1.01f)
		{
			t += Time.deltaTime;
			float y = Mathf.LerpAngle(startAng, 0, t);
			menuView.rectTransform.eulerAngles = new Vector3(0, y, 0);
			yield return null;
		}
		menuView.rectTransform.eulerAngles = new Vector3(0, 0, 0);
	}

	protected IEnumerator FlipOut()
	{
		float startAng = menuView.rectTransform.eulerAngles.y;
		float t = 0;
		while (t < 1.01f)
		{
			t += Time.deltaTime;
			float y = Mathf.LerpAngle(startAng, -90, t);
			menuView.rectTransform.eulerAngles = new Vector3(0, y, 0);
			yield return null;
		}
		menuView.rectTransform.eulerAngles = new Vector3(0, -90, 0);
	}
	*/
}
