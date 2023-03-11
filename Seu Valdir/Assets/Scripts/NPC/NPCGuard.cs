using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCGuard : MonoBehaviour
{
	public PlayerStealthController playerTarget;
	public NPCWay way;
	public Transform current;
	int index = 1;
	int indexDirection;
	float speed;
	Vector3 direction;
	bool looking;

	public void Init(PlayerStealthController player)
	{
		speed = 3;
		indexDirection = 1;
		index = 1;
		transform.position = way.wayPoints[index].position;
		current = way.wayPoints[index];
		playerTarget = player;
	}

	public void MoveTo()
	{
		direction = (way.wayPoints[index].position - transform.position).normalized;
		if (direction.sqrMagnitude != 0)
		{ transform.rotation = Quaternion.LookRotation(direction); }
		transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}

	public void FrameUpdate()
	{
		if (looking)
		{ Watch(); }
		else
		{
			MoveTo();
			NextPoint();
			Watch();
		}
	}

	void NextPoint()
	{
		float d = Vector3.Distance(transform.position, way.wayPoints[index].position);
		if (d < 0.05f)
		{
			looking = LookAround();
			if (looking)
			{
				transform.eulerAngles = way.wayPoints[index].eulerAngles;
				StartCoroutine(WatchInterval());
				return;
			}

			if (Random.Range(0, 11) > 4) { indexDirection *= -1; }
			if ((index >= way.wayPoints.Count - 1 && indexDirection > 0) ||
				(index <= 1 && indexDirection < 0))
			{
				indexDirection *= -1;
				return;
			}
			index += indexDirection;
		}
		current = way.wayPoints[index];
	}

	void Watch()
	{
		//transform.eulerAngles = way.wayPoints[index].eulerAngles;
		Vector3 dir = transform.forward;
		if(transform.position.y > 2)
		{ dir.y -= transform.position.y / 3f; dir.Normalize(); }

		if (WatchDirection(dir)) 
		{ playerTarget.caught = true; }
	}

	bool LookAround()
	{
		float r = DistanceRelation(way.wayPoints[index]);
		if(r > Random.Range(0, MeanDistance()))
		{
			return true;
		}
		return false;
	}

	float DistanceRelation(Transform current)
	{
		float total = CumulativeDistance();
		return total / (playerTarget.transform.position - current.position).magnitude;// / total;
	}

	float CumulativeDistance()
	{
		float total = 0;
		for (int i = 0; i < way.wayPoints.Count; ++i)
		{
			total += (playerTarget.transform.position - way.wayPoints[i].position).magnitude;
		}
		return total;
	}

	float MeanDistance()
	{
		return CumulativeDistance() / way.wayPoints.Count;
	}

	IEnumerator WatchInterval()
	{
		yield return new WaitForSeconds(2f);
		looking = false;
	}

	bool WatchDirection(Vector3 dir)
	{
		Vector3 d = (playerTarget.transform.position - transform.position).normalized;
		RaycastHit[] hits = Physics.RaycastAll(transform.position, d, 30f);
		//Debug.DrawRay(transform.position, d * 50f, Color.white);
		if(hits.Length == 1 && transform.eulerAngles.y == 180)
		{ print("pego"); return true; }

		//Debug.DrawRay(transform.position, d * 50f, Color.white);
		//Debug.DrawRay(transform.position + d * 50f, dir * 5f, Color.white);
		hits = Physics.SphereCastAll(transform.position, 5f, dir, 30f);
		for (int i = 0; i < hits.Length; ++i)
		{
			if (hits[i].collider.gameObject.CompareTag("Player"))
			{
				if (playerTarget.direction.magnitude != 0)
				{ print("pego"); return true; }
			}
		}
		return false;
	}
}
