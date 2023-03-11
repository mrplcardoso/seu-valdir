using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialCrossStreetState : CrossStreetState
{
	[SerializeField]
	TutorialMessage[] tutorialMessage;
	int messageIndex;
	Text tutorialText;
	Image tutorialImage;
	bool stopTutorial;

	private void Start()
	{
		tutorialBackground.color = Color.clear;
		tutorialText = tutorialBackground.GetComponentInChildren<Text>();
		tutorialImage = tutorialBackground.GetComponentsInChildren<Image>()[1];
	}

	public override void OnEnter()
	{
		GameCrossInformation.currentEvent = 2;
		messageIndex = 0;
		stopTutorial = false;
		StartCoroutine(ShowTutorial());
	}

	IEnumerator ShowTutorial()
	{
		while (messageIndex < tutorialMessage.Length)
		{
			tutorialText.text = tutorialMessage[messageIndex].text;
			if (tutorialMessage[messageIndex].image == null)
			{ tutorialImage.gameObject.SetActive(false); }
			else
			{
				tutorialImage.gameObject.SetActive(true);
				tutorialImage.sprite = tutorialMessage[messageIndex].image;
				tutorialImage.rectTransform.sizeDelta = new Vector2(
					tutorialImage.rectTransform.sizeDelta.x * 2.5f,
					tutorialImage.rectTransform.sizeDelta.y);
			}

			if (stopTutorial) { yield break; }
			yield return FlipIn(tutorialImage, tutorialText);
			yield return new WaitForSeconds(4f);

			yield return FlipOut(1, tutorialImage, tutorialText);
			tutorialText.text = "";
			tutorialImage.sprite = null;
			++messageIndex;
			yield return new WaitForSeconds(0.5f);
		}
		crossStreetMachine.ChangeState<StartCrossStreetState>();
	}

	protected override IEnumerator FlipIn(Image img = null, Text txt = null)
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
			if (stopTutorial) { yield break; }
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

	protected override IEnumerator FlipOut(float incTime = 1f, Image img = null, Text txt = null)
	{
		Color c = tutorialBackground.color;
		Color cImg = (img != null) ? img.color : Color.clear;
		Color cTxt = (txt != null) ? txt.color : Color.clear;
		Color end = Color.clear;

		float t = 0;
		while (t < 1.01f)
		{
			if (stopTutorial) { yield break; }
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

	private void Update()
	{
		if (Input.GetAxisRaw("Fire1") != 0 && !stopTutorial)
		{
			StopAllCoroutines();
			StartCoroutine(Change());
			stopTutorial = true;
		}
	}

	IEnumerator Change()
	{
		yield return base.FlipOut(2f, tutorialImage, tutorialText);
		tutorialText.text = "";
		tutorialImage.sprite = null;
		yield return new WaitForSeconds(0.5f);
		crossStreetMachine.ChangeState<StartCrossStreetState>();
		this.enabled = false;
	}

	public override void OnExit()
	{
		tutorialImage.gameObject.SetActive(false);
	}

	[Serializable]
	private class TutorialMessage
	{
		public string text;
		public Sprite image;
		public Vector2 imageSize;
	}
}
