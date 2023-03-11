using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEventTrigger : MonoBehaviour
{
	[HideInInspector]
	public bool runEvent;
	private void Start()
	{
		runEvent = false;
	}

	public void OverLapEventTrigger()
	{
		Collider[] c = Physics.OverlapBox(transform.position, Vector3.one * 4);
		for(int i = 0; i < c.Length; ++i)
		{
			if(c[i].gameObject.CompareTag("EventTrigger"))
			{
				runEvent = true;
				return;
			}
		}
	}
}
