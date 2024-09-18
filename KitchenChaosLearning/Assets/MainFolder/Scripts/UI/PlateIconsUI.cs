using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour
{
	[SerializeField] private PlateKitchenObject plateKitchenObject;

	[SerializeField] private GameObject plateIconTemplate;

	private void Awake()
	{		
		plateIconTemplate.SetActive(false);
	}

	private void Start()
	{
		plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
	}

	private void PlateKitchenObject_OnIngredientAdded(KitchenObjectSO kitchenObjectSO1)
	{
		UpdateVisual();
	}

	private void UpdateVisual()
	{
		foreach (Transform child in transform)
		{
			if (child.gameObject == plateIconTemplate)
				continue;
			Destroy(child.gameObject);
		}

		foreach (KitchenObjectSO kitchenObjectSO in plateKitchenObject.GetIngredientList())
		{
			GameObject plateIcon = Instantiate(plateIconTemplate, transform);
			plateIcon.GetComponent<IconTemplateUI>().SetIconSprite(kitchenObjectSO.sprite);
			plateIcon.SetActive(true);
		}
	}
}
