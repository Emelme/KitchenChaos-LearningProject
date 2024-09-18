using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
	[SerializeField] private PlateKitchenObject plate;

	[Serializable] private struct PlateCompleteVisualGameObject_KitchneObjectSO
	{
		public GameObject plateCompleteVisualGameObject;
		public KitchenObjectSO kitchenObjectSO;
	}

	[SerializeField] private List<PlateCompleteVisualGameObject_KitchneObjectSO> plateCompleteVisualGameObjectList;

	private void Start()
	{
		plate.OnIngredientAdded += Plate_OnIngredientAdded;

		foreach (PlateCompleteVisualGameObject_KitchneObjectSO plateCompleteVisualGameObject in plateCompleteVisualGameObjectList)
		{
			plateCompleteVisualGameObject.plateCompleteVisualGameObject.SetActive(false);
		}
	}

	private void Plate_OnIngredientAdded(KitchenObjectSO kitchenObjectSO)
	{
		foreach (PlateCompleteVisualGameObject_KitchneObjectSO plateCompleteVisualGameObject in plateCompleteVisualGameObjectList)
		{
			if (plateCompleteVisualGameObject.kitchenObjectSO == kitchenObjectSO)
			{
				plateCompleteVisualGameObject.plateCompleteVisualGameObject.SetActive(true);
			}
		}
	}
}
