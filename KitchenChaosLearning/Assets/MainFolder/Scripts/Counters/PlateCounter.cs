using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : Counter
{
	[SerializeField] private KitchenObjectSO plateKitchenObjectSO;

	[SerializeField] private float spawnPlateTime;

	[SerializeField] private int maxPlateAmount;
	private int plateAmount = 0;

	private bool isCoroutineRunning = false;

	public event Action OnPlateSpawn;
	public event Action OnPlateRemoved;

	private void Start()
	{
		StartCoroutine(StartSpawningPlate());
	}

	private IEnumerator StartSpawningPlate()
	{
		isCoroutineRunning = true;
		yield return new WaitForSeconds(spawnPlateTime);
		isCoroutineRunning = false;

		if (plateAmount < maxPlateAmount)
		{
			OnPlateSpawn?.Invoke();

			plateAmount++;

			yield return StartCoroutine(StartSpawningPlate());
		}
	}

	public override void Interact()
	{
		if (!Player.Instance.HasKitchenObject() && plateAmount > 0)
		{
			plateAmount--;

			OnPlateRemoved?.Invoke();

			KitchenObject.CreateKitchenObject(plateKitchenObjectSO, Player.Instance);

			if (!isCoroutineRunning)
			{
				StartCoroutine(StartSpawningPlate());
			}
		}
	}
}
