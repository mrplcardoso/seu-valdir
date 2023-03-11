using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabBuildingManager : MonoBehaviour
{
	public List<BaseObject> objects;

	void Update()
	{
		foreach(BaseObject ob in objects)
		{ ob.FrameUpdate(); }
	}

	private void FixedUpdate()
	{
		foreach (BaseObject ob in objects)
		{ ob.PhysicsUpdate(); }
	}
}
