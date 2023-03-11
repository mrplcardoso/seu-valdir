using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenMirrorPlayerController : BaseObject
{
	public BaseObject bulletPrefab;
	public GameObject estilingue;
	public Transform bulletFire;

	Animator estilingueAnim;
	AudioSource rockAudio;

	RockObject currentRock;

	public int mirros;
	public float mouseSpeed;
	bool canFire;

	private void Start()
	{
		estilingue = GetComponentsInChildren<Transform>()[1].gameObject;
		estilingueAnim = estilingue.GetComponent<Animator>();
		rockAudio = estilingue.GetComponent<AudioSource>();
		canFire = true;

		currentRock = (RockObject)Instantiate(bulletPrefab, bulletFire.position, transform.rotation, transform);
		currentRock.player = this;
	}

	public override void FrameUpdate()
	{
		MouseRotation();
		LaunchRock();
	}

	public override void PhysicsUpdate()
	{

	}

	public override void PostUpdate()
	{

	}

	void MouseRotation()
	{
		float x = Input.GetAxis("Mouse X");
		float y = Input.GetAxis("Mouse Y");
		Vector3 axis = new Vector3(transform.eulerAngles.x - y, transform.eulerAngles.y + x, 0);
		axis.x = Mathf.Clamp(axis.x, 315, 350);
		transform.eulerAngles = axis;
	}

	void LaunchRock()
	{
		if (Input.GetMouseButtonDown(0) && canFire)
		{
			StartCoroutine(TriggerBullet());
		}
	}

	IEnumerator RockTrackBack()
	{
		float t = 0;
		Vector3 s = currentRock.transform.position;
		Vector3 end = currentRock.transform.position - (currentRock.transform.forward * 0.18f);
		while (t < 1.01f)
		{
			currentRock.transform.position = Vector3.Lerp(s, end, t);
			t += 2.5f * Time.deltaTime;
			yield return null;
		}
	}

	IEnumerator TriggerBullet()
	{
		canFire = false;
		estilingueAnim.SetTrigger("Puxando");
		yield return RockTrackBack();
		//yield return new WaitForSeconds(0.5f);

		rockAudio.Play();

		currentRock.transform.parent = null;
		currentRock.GetComponent<Rigidbody>().AddForce(transform.forward * 75f);
		currentRock.GetComponent<Rigidbody>().useGravity = true;

		yield return new WaitForSeconds(0.2f);
		currentRock = (RockObject)Instantiate(bulletPrefab, bulletFire.position, transform.rotation, transform);
		currentRock.player = this;
		canFire = true;
	}

	public bool BrokeAllMirrors()
	{
		if (mirros == 16) return true;
		return false;
	}
}
