using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrombadinhaObject : BaseObject
{
	public TrombadinhaController controller;
	Animator animator;
	public bool renderize
	{ 
		get { return animator.gameObject.activeInHierarchy; }
		set { animator.gameObject.SetActive(value); }
	}

	public float correndoAnim
	{ set { animator.SetFloat("Correndo", value); } }

	public string setTriggerAnim
	{ set { animator.SetTrigger(value); } }

	private void Start()
	{
		controller = GetComponent<TrombadinhaController>();
		animator = GetComponentInChildren<Animator>(true);
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
}
