using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
	[SerializeField] private AudioSource audioSource;
	[SerializeField] private AudioClipRefsSO audioClipRefsSO;
	private AudioClip[] footsteps;
	[SerializeField] private float footstepTime;

	private void Start()
	{
		footsteps = audioClipRefsSO.footstep;

		StartCoroutine(PlayFootstepSound());
	}

	private IEnumerator PlayFootstepSound()
	{
		while (true)
		{
			//if (Player.Instance.IsWalking)
			{
				audioSource.clip = footsteps[Random.Range(0, footsteps.Length)];
				audioSource.volume = SoundManager.Instance.GetVolumeMultiplier();
				audioSource.Play();
				yield return new WaitForSeconds(footstepTime);
				audioSource.Pause();
			}

			yield return null;
		}
	}
}
