using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
	Animator animator;

	public float andandoAnim
	{ 
		get { return animator.GetFloat("Andando"); }
		set { animator.SetFloat("Andando", value); }
	}

	public string setTriggerAnim
	{
		set { animator.SetTrigger(value); }
	}

	public float currentAnimationTime
	{ get { return animator.GetCurrentAnimatorStateInfo(0).normalizedTime; } }

	private void Start()
	{
		animator = GetComponentInChildren<Animator>();
	}
}
