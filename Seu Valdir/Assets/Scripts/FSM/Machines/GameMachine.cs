using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMachine : StateMachine
{
	List<BaseObject> objectsList;
	public PlayerObject player;

	void Start()
	{
		objectsList = new List<BaseObject>();
		ChangeState<InitializeGameState>();
	}

	public int AddToProcess(BaseObject obj)
	{
		if(!objectsList.Contains(obj))
		{
			objectsList.Add(obj);
			return objectsList.Count - 1;
		}
		return -1;
	}

	public void ElementsUpdate()
	{
		for (int i = 0; i < objectsList.Count; ++i)
		{
			objectsList[i].FrameUpdate();
		}
	}
	public void ElementsPhysicsUpdate()
	{
		for (int i = 0; i < objectsList.Count; ++i)
		{
			objectsList[i].PhysicsUpdate();
		}
	}

	public void ElementsPostUpdate()
	{
		for (int i = 0; i < objectsList.Count; ++i)
		{
			objectsList[i].PostUpdate();
		}
	}

	public void CheckForEvent()
	{
		if(player.playerEvent.runEvent)
		{
			player.playerEvent.runEvent = false;

			switch (GameCrossInformation.currentEvent)
			{
				case 0:
				{
					ChangeState<BrokenMirrorGameState>();
					break;
				}
				case 1:
				{
					ChangeState<CrossStreetGameState>();
					break;
				}
				case 2:
				{
					ChangeState<StealthGameState>();
					break;
				}
			}
		}
	}
}
