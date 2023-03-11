using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGameController : MonoBehaviour
{
	public GameObject waldirModel;
	Animator waldirAnim;
	public Camera mainCamera;
	public Light spotLight;
	public Image fadeImageText;
	public Text endText;
	public Color fadeImageColor;
	Color textColor;
	public List<string> messages;

	int messageIndex;

	private void Start()
	{
		textColor = endText.color;
		endText.color = Color.clear;
		waldirModel.transform.eulerAngles = Vector3.zero;
		spotLight.intensity = 0;
		messageIndex = 0;

		waldirAnim = waldirModel.GetComponent<Animator>();

		StartCoroutine(EndThread());
	}

	IEnumerator EndThread()
	{
		yield return TurnLightOn();
		yield return ZoomInCamera();
		yield return WaldirFace();

		while (messageIndex < messages.Count)
		{
			endText.text = messages[messageIndex];
			yield return FadeImageIn();
			yield return FadeImageOut();
			messageIndex++;
		}
		waldirAnim.SetTrigger("Morrendo");
		yield return new WaitForSeconds(7f);
		SceneManager.LoadScene("MainMenu");
	}

	IEnumerator TurnLightOn()
	{
		float s = spotLight.intensity;
		float e = 7f;
		float t = 0;
		while(t < 1.01f)
		{
			spotLight.intensity = Mathf.Lerp(s, e, t);
			t += 1 * Time.deltaTime;
			yield return null;
		}
	}

	IEnumerator ZoomInCamera()
	{
		Vector3 start = mainCamera.transform.position;
		Vector3 end = mainCamera.GetComponentsInChildren<Transform>()[1].position;

		float t = 0;
		while(t < 1.01f)
		{
			mainCamera.transform.position = Vector3.Lerp(start, end, t);
			t += 0.5f * Time.deltaTime;
			yield return null;
		}
		mainCamera.transform.position = end;
	}

	IEnumerator WaldirFace()
	{
		Vector3 euler = waldirModel.transform.eulerAngles;
		float s = waldirModel.transform.eulerAngles.y;
		float e = 180;
		float t = 0;

		while(t < 1.01f)
		{
			euler.y = Mathf.Lerp(s, e, t);
			waldirModel.transform.eulerAngles = euler;
			t += 1 * Time.deltaTime;
			yield return null;
		}
		euler.y = 180;
		waldirModel.transform.eulerAngles = euler;
	}

	IEnumerator FadeImageIn()
	{
		Color c = fadeImageText.color;
		Color tc = endText.color;
		float t = 0;

		while (t < 1.01f)
		{
			fadeImageText.color = Color.Lerp(c, fadeImageColor, t);
			endText.color = Color.Lerp(tc, textColor, t);
			t += 1f * Time.deltaTime;
			yield return null;
		}
		yield return new WaitForSeconds(6f);
	}

	IEnumerator FadeImageOut()
	{
		Color c = fadeImageText.color;
		Color tc = endText.color;
		float t = 0;

		while (t < 1.01f)
		{
			fadeImageText.color = Color.Lerp(c, Color.clear, t);
			endText.color = Color.Lerp(tc, Color.clear, t);
			t += 1f * Time.deltaTime;
			yield return null;
		}
		yield return new WaitForSeconds(0.5f);
	}
}
