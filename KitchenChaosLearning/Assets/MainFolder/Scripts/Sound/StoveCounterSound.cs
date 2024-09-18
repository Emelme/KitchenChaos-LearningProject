using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
	[SerializeField] private AudioSource audioSource;
	[SerializeField] private StoveCounter stoveCounter;

	private void Start()
	{
		stoveCounter.OnInteracted += StoveCounter_OnInteracted;
	}

	private void StoveCounter_OnInteracted()
	{
		if (stoveCounter.HasKitchenObject())
		{
			audioSource.volume = SoundManager.Instance.GetVolumeMultiplier();
			audioSource.Play();
		}
		else
		{
			audioSource.Pause();
		}
	}
}
