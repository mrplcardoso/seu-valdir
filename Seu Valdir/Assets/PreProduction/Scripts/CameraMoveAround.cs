using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveAround : MonoBehaviour
{
	public GameObject target;
	[Range(0.0f, 100.0f)]
	public float mouseSensitivity = 100f;
	float zoomSpacing = 5;

	public bool lockX, lockY;
	public bool moveAlong;

	private void Update()
	{ 
		float mousex = (!lockX) ? Input.GetAxisRaw("Mouse X") : 0f;
		float mousey = (!lockY) ? Input.GetAxisRaw("Mouse Y") : 0f;
		float wheel = Input.GetAxis("Mouse ScrollWheel");

		if (Input.GetAxisRaw("Fire1") != 0)
		{
			if(Mathf.Abs(mousex) < 0.3f) { mousex = 0; }
			if (Mathf.Abs(mousey) < 0.3f) { mousey = 0; }

			transform.RotateAround(target.transform.position, new Vector3(0, mousex, 0), mouseSensitivity * Time.deltaTime);
			transform.RotateAround(target.transform.position, new Vector3(mousey, 0, 0), -mouseSensitivity * Time.deltaTime);
		}
		if (moveAlong)
		{
			float d = Vector3.Distance(transform.position, target.transform.position);
			if (d > zoomSpacing)
			{
				Vector3 n = transform.position +
					(target.transform.position - transform.position).normalized * d;
				transform.position = new Vector3(n.x, transform.position.y, n.z);
			}
			transform.position = new Vector3(target.transform.position.x, transform.position.y,
				target.transform.position.z - zoomSpacing);
		}
		if (wheel > 0)
		{ transform.Translate(Vector3.forward * zoomSpacing * Time.deltaTime); }
		if(wheel < 0)
		{ transform.Translate(Vector3.forward * -zoomSpacing * Time.deltaTime); }
	}

	private void LateUpdate()
	{

	}
}
