using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeGameState : GameState
{
	public Transform[] playerStart;
	Transform[] playerWay;
	public Transform cameraStart;
	Transform[] cameraWay;

	int placedPlayer;

	public void Start()
	{
		//GameCrossInformation.currentEvent = 0;
		placedPlayer = 0;
		playerWay = playerStart[0].gameObject.GetComponentsInChildren<Transform>();
		cameraWay = cameraStart.gameObject.GetComponentsInChildren<Transform>();
		StartCoroutine(InstantiateObjects());
	}

	IEnumerator InstantiateObjects()
	{
		CreatePlayer();
		yield return null;
		EventInstance();
	}

	void CreatePlayer()
	{
		//gameMachine.player.playerCamera = 
		Instantiate(gameMachine.player.playerCamera,
			cameraStart.position, cameraStart.rotation);
		gameMachine.player = Instantiate(gameMachine.player, playerStart[0].position, playerStart[0].rotation);
	}

	IEnumerator LerpWayPoints(Transform obj, Transform[] wayPoints, float time)
	{
		for(int i = 1; i < wayPoints.Length; ++i)
		{
			yield return LerpRoutine(obj, wayPoints[i].position, time);
		}
		placedPlayer++;
	}

	IEnumerator LerpRotation(Transform start, float end, float time)
	{
		Quaternion s = start.rotation;
		Quaternion e = Quaternion.Euler(Vector3.up * end);
		float t = 0;
		while(t < 1.01f)
		{
			start.rotation = Quaternion.Lerp(s, e, t);
			t += Time.deltaTime * time;
			yield return null;
		}
		placedPlayer++;
	}

	IEnumerator LerpRoutine(Transform start, Vector3 end, float time)
	{
		float t = 0;
		Vector3 s = start.position;
		while(t < 1.01f)
		{
			start.position = Vector3.Lerp(s, end, t);
			t += Time.deltaTime * time;
			yield return null;
		}
	}

	IEnumerator WaitIntroAnimation()
	{
		gameMachine.player.playerController.audioSource.loop = true;
		gameMachine.player.playerController.audioSource.Play();
		yield return new WaitWhile(() => placedPlayer < 4);
		gameMachine.player.playerController.audioSource.Stop();
		gameMachine.player.playerController.audioSource.loop = false;
		gameMachine.player.playerAnimation.andandoAnim = 0;
		gameMachine.ChangeState<UpdateGameState>();
	}

	public override void OnExit()
	{
		gameMachine.player.playerCamera.transform.parent = gameMachine.player.playerCamera.playerHead;
		gameMachine.player.lockCamera = false;
		gameMachine.AddToProcess(gameMachine.player);
	}

	void EventInstance()
	{
		switch (GameCrossInformation.currentEvent)
		{
			case 0:
			{
				StartCoroutine(LerpWayPoints(gameMachine.player.transform, playerWay, 0.5f));
				gameMachine.player.playerAnimation.andandoAnim = 1;
				StartCoroutine(LerpWayPoints(gameMachine.player.playerCamera.transform, cameraWay, 0.5f));
				StartCoroutine(LerpRotation(gameMachine.player.transform, 90, 0.25f));
				StartCoroutine(LerpRotation(gameMachine.player.playerCamera.transform, 90, 0.25f));

				StartCoroutine(WaitIntroAnimation());
				break;
			}
			case 1:
			{
				gameMachine.player.transform.position = playerStart[1].position;
				gameMachine.player.transform.rotation = playerStart[1].rotation;
				gameMachine.player.playerCamera.transform.position = new Vector3(playerStart[1].position.x,
					cameraWay[cameraWay.Length-1].position.y - playerStart[1].position.y,
					playerStart[1].position.z -13.22f);
				gameMachine.player.playerCamera.transform.rotation = playerStart[1].rotation;
				gameMachine.ChangeState<UpdateGameState>();
				break;
			}
			case 2:
			{
				gameMachine.player.transform.position = playerStart[2].position;
				gameMachine.player.transform.rotation = playerStart[2].rotation;
				gameMachine.player.playerCamera.transform.position = new Vector3(playerStart[2].position.x,
					cameraWay[cameraWay.Length - 1].position.y - playerStart[2].position.y,
					playerStart[2].position.z - 13.22f);
				gameMachine.player.playerCamera.transform.rotation = playerStart[2].rotation;
				gameMachine.ChangeState<UpdateGameState>();
				break;
			}
		}
	}
}
