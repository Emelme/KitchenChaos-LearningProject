using UnityEngine;

public class ClearCounter : Counter
{
	public override void Interact()
	{
		if (Player.Instance.HasKitchenObject())
		{
			if (HasKitchenObject())
			{
				if (Player.Instance.GetKitchenObject().TryGetPlate(out PlateKitchenObject plate))
				{
					if (plate.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
					{
						GetKitchenObject().DestroySelf();
					}
				}
				else if (GetKitchenObject().TryGetPlate(out plate))
				{
					if (plate.TryAddIngredient(Player.Instance.GetKitchenObject().GetKitchenObjectSO()))
					{
						Player.Instance.GetKitchenObject().DestroySelf();
					}
				}
			}
			else
			{
				Player.Instance.GetKitchenObject().SetKitchenObjectParent(this);
			}
		}
		else
		{
			if (HasKitchenObject())
			{
				GetKitchenObject().SetKitchenObjectParent(Player.Instance);
			}
		}
	}
}
