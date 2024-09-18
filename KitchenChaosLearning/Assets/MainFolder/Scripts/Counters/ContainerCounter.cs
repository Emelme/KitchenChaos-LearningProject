using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : Counter
{
	[SerializeField] private KitchenObjectSO kitchenObjectSO;

	public event Action OnContainerCounterInteracted;

	public override void Interact()
	{
		if (!Player.Instance.HasKitchenObject())
		{
			KitchenObject.CreateKitchenObject(kitchenObjectSO, Player.Instance);

			OnContainerCounterInteracted?.Invoke();
		}
		else if (Player.Instance.GetKitchenObject().TryGetPlate(out PlateKitchenObject plate))
		{
			if (plate.TryAddIngredient(kitchenObjectSO))
			{
				OnContainerCounterInteracted?.Invoke();
			}
		}
	}
}
