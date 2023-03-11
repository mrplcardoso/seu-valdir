using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
	public Transform playerBody;
	public Transform playerHead;

	float angle = 3f;

	public void RotateAround()
	{
		if (playerBody != null)
		{ playerBody.transform.Rotate(Vector3.up, angle * Input.GetAxis("Mouse X")); }
	}

	public void RotateAround(float newAngle)
	{
		if (playerBody != null)
		{ playerBody.transform.Rotate(Vector3.up, newAngle); }
	}
}
