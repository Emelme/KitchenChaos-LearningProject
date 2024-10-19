using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
	[SerializeField] private Animator animator;

	private const string IS_WALKING = "isWalking";

	private void Update()
	{
		animator.SetBool(IS_WALKING, Player.Instance.IsWalking);
	}
}
