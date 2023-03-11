using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawnTrigger : MonoBehaviour
{
	public List<NPCObject> npcPrefabs;
	public NPCWay[] npcStart;

	public List<NPCObject> npcList;

	public int npcIndex;
	public bool instancing;

	public void SpawnStart()
	{
		npcIndex = 0;
		npcList = new List<NPCObject>();
		for (int i = 0; i < 100; ++i)
		{
			int r = Random.Range(0, npcPrefabs.Count);
			NPCObject n = Instantiate(npcPrefabs[r], Vector3.zero, Quaternion.identity);
			npcList.Add(n);
			n.gameObject.SetActive(false);
		}
	}

	public Coroutine SpawnTrigger(float interval)
	{
		return StartCoroutine(Spawn(interval));
	}

	public IEnumerator Spawn(float interval)
	{
		while(true)
		{
			if(instancing)
			{
				if(npcIndex > npcList.Count-1)
				{ npcIndex = 0; }
				NPCObject n = npcList[npcIndex];
				if(!n.active)
				{
					n.active = true;
					int r1 = Random.Range(0, npcStart.Length);
					n.Init(npcStart[r1].wayPoints);
				}
				npcIndex++;
			}
			yield return new WaitForSeconds(interval);
		}
	}

	public void UpdateNPC()
	{
		if (!instancing) return;
		for(int i = 0; i < npcList.Count; ++i)
		{
			if (!npcList[i].active) continue;
			npcList[i].FrameUpdate();
		}
	}
}
