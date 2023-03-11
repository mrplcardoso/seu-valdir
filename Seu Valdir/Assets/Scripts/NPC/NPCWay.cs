using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCWay : MonoBehaviour
{
	public List<Transform> wayPoints { get; private set; }
	public Transform start { get { return wayPoints[0]; } }

	private void Start()
	{
		wayPoints = new List<Transform>();
		Transform[] p = GetComponentsInChildren<Transform>();
		for(int i = 0; i < p.Length; ++i)
		{
			wayPoints.Add(p[i]);
		}
	}
}
