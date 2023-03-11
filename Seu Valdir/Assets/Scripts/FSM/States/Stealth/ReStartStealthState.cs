using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReStartStealthState : StealthState
{
	public UpdateStealthState updateState;
	public Vector3 playerStartPosition;
	public Quaternion playerStartRotation;
	public List<Vector3> guardStartPosition;
	public List<Quaternion> guardStartRotation;

	public Image fadeImage;
	AudioSource sceneAudio;

	private void Start()
	{
		updateState = stealthMachine.HasState<UpdateStealthState>();
		sceneAudio = GetComponent<AudioSource>();

		playerStartPosition = updateState.player.transform.position;
		playerStartRotation = updateState.player.transform.rotation;

		guardStartPosition = new List<Vector3>();
		guardStartRotation = new List<Quaternion>();

		guardStartPosition.Add(updateState.guards[0].transform.position);
		guardStartRotation.Add(updateState.guards[0].transform.rotation);

		guardStartPosition.Add(updateState.guards[1].transform.position);
		guardStartRotation.Add(updateState.guards[1].transform.rotation);
	}

	public override void OnEnter()
	{
		StartCoroutine(Fade());
	}

	IEnumerator Fade()
	{
		sceneAudio.Play();

		yield return new WaitForSeconds(1f);

		Color c = fadeImage.color;
		float t = 0;
		
		while(t < 1.01f)
		{
			fadeImage.color = Color.Lerp(c, Color.black, t);
			t += 1f * Time.deltaTime;
			yield return null;
		}

		yield return new WaitForSeconds(0.5f);

		updateState.player.caught = false;
		updateState.player.transform.position = playerStartPosition;
		updateState.player.transform.rotation = playerStartRotation;

		updateState.guards[0].transform.position = guardStartPosition[0];
		updateState.guards[0].transform.rotation = guardStartRotation[0];

		updateState.guards[1].transform.position = guardStartPosition[1];
		updateState.guards[1].transform.rotation = guardStartRotation[1];

		c = fadeImage.color;
	  t = 0;

		while (t < 1.01f)
		{
			fadeImage.color = Color.Lerp(c, Color.clear, t);
			t += 1f * Time.deltaTime;
			yield return null;
		}
		yield return new WaitForSeconds(0.5f);
		stealthMachine.ChangeState<UpdateStealthState>();
	}
}
