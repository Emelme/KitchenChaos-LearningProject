using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningUISound : MonoBehaviour
{
	[SerializeField] private AudioClip[] warningSoundArray;

	public void PlayWarningSound()
	{
		AudioSource.PlayClipAtPoint(warningSoundArray[Random.Range(0, warningSoundArray.Length)], transform.position, SoundManager.Instance.GetVolumeMultiplier());
	}
}
