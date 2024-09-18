using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : Counter
{
	public override void Interact()
	{
		if (Player.Instance.HasKitchenObject())
		{
			if (Player.Instance.GetKitchenObject().TryGetPlate(out PlateKitchenObject plate))
			{
				DeliveryManager.Instance.HandleDelivery(plate);

				plate.DestroySelf();
			}
		}
	}
}
