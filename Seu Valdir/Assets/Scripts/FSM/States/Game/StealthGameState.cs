using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StealthGameState : GameState
{
	public TrombadinhaObject trombadinha;
	public Transform triggerObject;
	public Transform trombadinhaStart;
	public Transform playerEnd;
	bool eventScene;
	bool waitAnimation;

	private void Start()
	{
		if (GameCrossInformation.currentEvent != 2)
		{
			triggerObject.gameObject.SetActive(false);
			return;
		}
		trombadinhaStart = triggerObject.GetComponentsInChildren<Transform>(true)[2];
		playerEnd = triggerObject.GetComponentsInChildren<Transform>(true)[1];
		trombadinha = Instantiate(trombadinha, trombadinhaStart.position, trombadinhaStart.rotation);
	}

	public override void OnEnter()
	{
		//StartCoroutine(RunIntroEvent());
		gameMachine.player.playerController.velocity = triggerObject.forward * 2f;
		/*gameMachine.player.playerController.velocity = (playerEnd.position - 
			gameMachine.player.transform.position).normalized;*/
		gameMachine.player.playerAnimation.andandoAnim = 1;

		SetTrombadinha();
		base.OnEnter();
	}

	public override IEnumerator OnUpdate()
	{
		while (!eventScene)
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
		if (d < 2.5f)
		{
			trombadinha.setTriggerAnim = "Roubar";

			StartCoroutine(Robbery());

			waitAnimation = true;
			StartCoroutine(CallEventScene());
		}
	}

	IEnumerator Robbery()
	{
		if (playerEnd.position.x < gameMachine.player.transform.position.x)
		{ yield return new WaitForSeconds(0.5f); }//a direita
		else
		{ yield return new WaitForSeconds(0.1f); }//a esquerda
		trombadinha.transform.eulerAngles = new Vector3(0, 180, 0);
		trombadinha.controller.velocity = trombadinha.transform.forward;
		yield return new WaitForSeconds(0.3f);
		gameMachine.player.playerAnimation.setTriggerAnim = "Roubado";
		gameMachine.player.playerController.velocity = Vector3.zero;
		gameMachine.player.playerAnimation.andandoAnim = 0;
	}

	void SetTrombadinha()
	{
		Vector3 p = trombadinha.transform.position;
		if (playerEnd.position.x < gameMachine.player.transform.position.x)
		{ p.x = gameMachine.player.transform.position.x - 2f; }//player a direita
		else
		{ p.x = gameMachine.player.transform.position.x + 2f; }//player a esquerda
		trombadinha.renderize = true;
		trombadinha.transform.position = p;
		trombadinha.controller.velocity = trombadinha.transform.forward;
		trombadinha.controller.speed = 5f;
		trombadinha.correndoAnim = 1;
	}

	IEnumerator CallEventScene()
	{
		yield return new WaitForSeconds(5f);
		//GameCrossInformation.currentEvent++;
		SceneManager.LoadScene("StealthTransition");
	}
}
