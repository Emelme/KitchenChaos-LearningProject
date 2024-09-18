using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
	[SerializeField] private StoveCounter stoveCounter;

	[SerializeField] private GameObject[] stoveCounterVisuals;

	private void Start()
	{
		stoveCounter.OnInteracted += StoveCounter_OnInteracted;
	}

	private void StoveCounter_OnInteracted()
	{
		if (stoveCounter.HasKitchenObject())
		{
			ShowStoveCounterVisuals();
		}
		else
		{
			HideStoveCounterVisuals();
		}
	}

	private void ShowStoveCounterVisuals()
	{
		foreach (GameObject stoveCounterVisual in stoveCounterVisuals)
		{
			stoveCounterVisual.SetActive(true);
		}
	}

	private void HideStoveCounterVisuals()
	{
		foreach (GameObject stoveCounterVisual in stoveCounterVisuals)
		{
			stoveCounterVisual.SetActive(false);
		}
	}
}
