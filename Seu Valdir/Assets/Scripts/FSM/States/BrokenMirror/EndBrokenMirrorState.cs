using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndBrokenMirrorState : BrokenMirrorState
{
	public Image fadeImage;

	public override void OnEnter()
	{
		StartCoroutine(FadeOut());
	}

	IEnumerator FadeOut()
	{
		Color c;
		while(fadeImage.color.a < 1)
		{
			c = fadeImage.color;
			c.a += Time.deltaTime;
			fadeImage.color = c;
			yield return null;
		}
		SceneManager.LoadScene("Game");
	}
}
