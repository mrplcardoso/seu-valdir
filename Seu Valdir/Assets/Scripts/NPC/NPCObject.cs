using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCObject : MonoBehaviour
{
	public List<Transform> target;
	public bool active 
	{
		get { return gameObject.activeInHierarchy; }
		set { gameObject.SetActive(value); }
	}
	int index = 0;
	float speed;
	Vector3 direction;

	public void Init(List<Transform> way)
	{
		speed = 14;
		index = 0;
		target = way;
		transform.position = target[0].position;
	}

	public void MoveTo()
	{
		//direction = (target[index].position - transform.position).normalized;
		transform.LookAt(target[index]);
		transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}

	public void FrameUpdate()
	{
		NextPoint();
		MoveTo();
	}

	void NextPoint()
	{
		float d = Vector3.Distance(transform.position, target[index].position);
		if(d < 0.25f)
		{
			if(index >= target.Count-1)
			{ 
				active = false;
				return;
			}
			++index;
		}
	}
}
