using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BrokenMirrorGameState : GameState
{
	public TrombadinhaObject trombadinha;
	public Transform triggerObject;
	public Transform trombadinhaStart;
	bool eventScene;
	bool waitAnimation;

	private void Start()
	{
		if(GameCrossInformation.currentEvent != 0)
		{
			triggerObject.gameObject.SetActive(false);
			return;
		}
		trombadinhaStart = triggerObject.GetComponentsInChildren<Transform>(true)[1];
		trombadinha = Instantiate(trombadinha, trombadinhaStart.position, trombadinhaStart.rotation);
	}

	public override void OnEnter()
	{
		StartCoroutine(RunIntroEvent());
		gameMachine.player.playerController.audioSource.loop = true;
		gameMachine.player.playerController.audioSource.Play();
		gameMachine.player.playerController.velocity = triggerObject.forward * 2f;
		gameMachine.player.playerAnimation.andandoAnim = 1;

		SetTrombadinha();
		base.OnEnter();
	}

	public override IEnumerator OnUpdate()
	{
		while(!eventScene)
		{
			if (!waitAnimation)
			{ TriggerAnimation(); }
			gameMachine.player.playerController.Move();
			trombadinha.controller.Move();
			yield return null;
		}
	}

	IEnumerator RunIntroEvent()
	{
		float t = 0;
		Quaternion s = gameMachine.player.playerCamera.transform.rotation;
		Quaternion e = Quaternion.Euler(Vector3.up * -90);
		while (t < 1.01f)
		{
			gameMachine.player.playerCamera.playerHead.rotation = Quaternion.Lerp(s, e, t);
			Vector3 p = gameMachine.player.playerCamera.transform.position;
			p.z -= 0.01f;
			gameMachine.player.playerCamera.transform.position = p;
			t += Time.deltaTime * 0.15f;
			yield return null;
		}
	}

	void TriggerAnimation()
	{
		float d = Vector3.Distance(gameMachine.player.transform.position, 
			trombadinha.transform.position);
		if(d < 3.5f)
		{
			gameMachine.player.playerController.audioSource.Stop();
			gameMachine.player.playerController.audioSource.loop = false;
			
			trombadinha.setTriggerAnim = "Chutando";
			gameMachine.player.playerAnimation.setTriggerAnim = "ChuteNaCanela";
			gameMachine.player.playerController.audioSource.clip =
				gameMachine.player.playerController.chutadoAudio;

			gameMachine.player.playerController.audioSource.Play();


			gameMachine.player.playerController.velocity = Vector3.zero;
			//gameMachine.player.playerController.speed = 0;
			gameMachine.player.playerAnimation.andandoAnim = 0;
			waitAnimation = true;
			StartCoroutine(CallEventScene());
		}
	}

	void SetTrombadinha()
	{
		Vector3 p = trombadinha.transform.position;
		p.x = gameMachine.player.transform.position.x - 0.25f;
		trombadinha.renderize = true;
		trombadinha.transform.position = p;
		trombadinha.controller.velocity = trombadinha.transform.forward;
		trombadinha.controller.speed = 5f;
		trombadinha.correndoAnim = 1;
	}

	IEnumerator CallEventScene()
	{
		yield return new WaitForSeconds(4f);
		//GameCrossInformation.currentEvent++;
		SceneManager.LoadScene("BrokenMirrorTransition");
	}
}
