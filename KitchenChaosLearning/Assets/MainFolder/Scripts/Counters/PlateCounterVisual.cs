using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlateCounterVisual : MonoBehaviour
{
	[SerializeField] private PlateCounter plateCounter;
	[SerializeField] private GameObject plateVisualPrefab;

	private Transform counterTopPoint;

	private List<GameObject> plateVisualList;

	private void Awake()
	{
		plateVisualList = new List<GameObject>();
	}

	private void Start()
	{
		counterTopPoint = plateCounter.GetKitchenObjectHoldPoint();

		plateCounter.OnPlateSpawn += PlateCounter_OnPlateSpawn;
		plateCounter.OnPlateRemoved += PlateCounter_OnPlateRemoved;
	}

	private void PlateCounter_OnPlateSpawn()
	{
		GameObject plateVisual = Instantiate(plateVisualPrefab, counterTopPoint);
		float plateSpawnOffsetY = 0.1f;
		plateVisual.transform.localPosition = new Vector3(0, plateSpawnOffsetY * plateVisualList.Count, 0);
		plateVisualList.Add(plateVisual);
	}

	private void PlateCounter_OnPlateRemoved()
	{
		Destroy(plateVisualList[plateVisualList.Count - 1]);
		plateVisualList.RemoveAt(plateVisualList.Count - 1);
	}
}
