using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
	public static MusicManager Instance { get; private set; }

	[SerializeField] private AudioSource audioSource;

	private float volumeMultiplier;
	private const string MUSIC_VOLUME_MULTIPLIER = "musicVolumeMultiplier";

	private void Awake()
	{
		Instance = this;

		volumeMultiplier = PlayerPrefs.GetFloat(MUSIC_VOLUME_MULTIPLIER, 0.5f);

		audioSource.volume = volumeMultiplier;
	}

	public void ChangeVolumeMultiplier()
	{
		volumeMultiplier += 0.1f;

		if (volumeMultiplier > 1.01f)
		{
			volumeMultiplier = 0f;
		}

		PlayerPrefs.SetFloat(MUSIC_VOLUME_MULTIPLIER, volumeMultiplier);
		PlayerPrefs.Save();

		audioSource.volume = volumeMultiplier;
	}

	public float GetVolumeMultiplierNormalized() { return Mathf.Round(volumeMultiplier * 10f); }
}
