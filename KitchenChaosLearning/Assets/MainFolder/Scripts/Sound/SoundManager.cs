using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
	public static SoundManager Instance { get; private set; }

	[SerializeField] private AudioClipRefsSO audioClipRefsSO;

	private float volumeMultiplier;
	private const string SOUND_VOLUME_MULTIPLIER = "soundVolumeMultiplier";

	private void Awake()
	{
		Instance = this;

		volumeMultiplier = PlayerPrefs.GetFloat(SOUND_VOLUME_MULTIPLIER, 0.5f);
	}

	private void Start()
	{
		DeliveryManager.Instance.OnDeliverySuccess += DeliveryManager_OnDeliverySuccess;
		DeliveryManager.Instance.OnDeliveryFailed += DeliveryManager_OnDeliveryFailed;

		CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;

		Player.Instance.OnObjectPicked += Player_OnObjectPicked;

		Counter.OnObjectDropped += Counter_OnObjectDropped;

		TrashCounter.OnObjectTrashed += TrashCounter_OnObjectTrashed;
	}

	private void TrashCounter_OnObjectTrashed(Vector3 position)
	{
		PlaySound(audioClipRefsSO.trash, position);
	}

	private void Counter_OnObjectDropped(Vector3 position)
	{
		PlaySound(audioClipRefsSO.objectDrop, position);
	}

	private void Player_OnObjectPicked()
	{
		PlaySound(audioClipRefsSO.objectPickup, Player.Instance.transform.position);
	}

	private void CuttingCounter_OnAnyCut(Vector3 position)
	{
		PlaySound(audioClipRefsSO.chop, position);
	}

	private void DeliveryManager_OnDeliverySuccess(Vector3 position)
	{
		PlaySound(audioClipRefsSO.deliverySuccess, position);
	}

	private void DeliveryManager_OnDeliveryFailed(Vector3 position)
	{
		PlaySound(audioClipRefsSO.deliveryFail, position);
	}


	private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
	{
		AudioSource.PlayClipAtPoint(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume * volumeMultiplier);
	}

	private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
	{
		AudioSource.PlayClipAtPoint(audioClip, position, volume * volumeMultiplier);
	}

	public void ChangeVolumeMultiplier()
	{
		volumeMultiplier += 0.1f;

		if (volumeMultiplier > 1.01f)
		{
			volumeMultiplier = 0f;
		}

		PlayerPrefs.SetFloat(SOUND_VOLUME_MULTIPLIER, volumeMultiplier);
		PlayerPrefs.Save();
	}

	public float GetVolumeMultiplier() { return volumeMultiplier; }
}
