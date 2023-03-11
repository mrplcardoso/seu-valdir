using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrossStreetGameState : GameState
{
	public Transform eventTrigger;

	private void Start()
	{

	}

	public override void OnEnter()
	{
		StartCoroutine(RotatePlayer());
	}

	IEnumerator RotatePlayer()
	{
		float t = 0;
		Quaternion end = Quaternion.Euler(Vector3.up * 90);
		gameMachine.player.playerAnimation.andandoAnim = 1;
		Vector3 start = gameMachine.player.transform.eulerAngles;

		while (gameMachine.player.transform.eulerAngles != end.eulerAngles)
		{
			gameMachine.player.transform.eulerAngles =
				Quaternion.Lerp(Quaternion.Euler(start), end, t += Time.deltaTime*0.27f).eulerAngles;

			gameMachine.player.playerController.velocity =
				2 * gameMachine.player.transform.forward;

			gameMachine.player.playerController.Move();
			yield return null;
		}
		gameMachine.player.playerAnimation.andandoAnim = 0;
		yield return new WaitForSeconds(0.5f);
		SceneManager.LoadScene("CrossStreet");
	}
}
