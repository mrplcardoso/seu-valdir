using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockObject : BaseObject
{
	public BrokenMirrorPlayerController player;
	public Material brokenMirrorMaterial;
	AudioSource breakGlass;

	private void Start()
	{
		breakGlass = GetComponent<AudioSource>();
	}

	public override void FrameUpdate()
	{

	}

	public override void PhysicsUpdate()
	{

	}

	public override void PostUpdate()
	{

	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.CompareTag("janela"))
		{
			breakGlass.Play();
			Renderer r = other.gameObject.GetComponent<Renderer>();
			if(!r.enabled)
			{
				r.enabled = true;
				player.mirros++;
			}
			//r.material = brokenMirrorMaterial;
			Destroy(this);
		}
	}
}