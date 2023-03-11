using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseController : MonoBehaviour
{
	[HideInInspector]
	public Vector3 velocity;
	public float speed;

	public abstract void Move();
}
