using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationLoopStart : MonoBehaviour
{
	public string animationState;
	Animator anim;

	// Start is called before the first frame update
	void Start()
	{
		anim = GetComponent<Animator>();

		float randomIdleStart = Random.Range(0, anim.GetCurrentAnimatorStateInfo(0).length); //Set a random part of the animation to start from
		anim.Play(animationState, 0, randomIdleStart);
	}
}
