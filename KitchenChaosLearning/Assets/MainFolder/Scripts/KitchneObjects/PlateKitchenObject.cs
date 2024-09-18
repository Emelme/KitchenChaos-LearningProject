using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
	[SerializeField] private List<KitchenObjectSO> validIngredientsList;

	private List<KitchenObjectSO> ingredientsList;

	public event Action<KitchenObjectSO> OnIngredientAdded;

	private void Awake()
	{
		ingredientsList = new List<KitchenObjectSO>();
	}

	public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO)
	{
		if (validIngredientsList.Contains(kitchenObjectSO) && !ingredientsList.Contains(kitchenObjectSO))
		{
			ingredientsList.Add(kitchenObjectSO);
			OnIngredientAdded(kitchenObjectSO);

			return true;
		}
		else
		{
			return false;
		}
	}

	public List<KitchenObjectSO> GetIngredientList() { return ingredientsList; }
}
