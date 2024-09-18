using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CuttingCounter : Counter
{
	[SerializeField] private CuttingRecipeSO[] cuttingRecipeSOs;

	private int cuttingProgress;

	public event Action<float> OnCuttingProgressChanged;
	public event Action OnCut;
	public static event Action<Vector3> OnAnyCut;

	new public static void ResetStaticData()
	{
		OnAnyCut = null;
	}

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
		else if (!Player.Instance.HasKitchenObject() && HasKitchenObject())
		{
			GetKitchenObject().SetKitchenObjectParent(Player.Instance);
			cuttingProgress = 0;
			OnCuttingProgressChanged?.Invoke(cuttingProgress);
		}
	}

	public override void InteractAlternate()
	{
		if (HasKitchenObject())
		{
			CuttingRecipeSO cuttingRecipeSO = FindCuttingRecipeSO(GetKitchenObject().GetKitchenObjectSO());

			if (cuttingRecipeSO != null)
			{
				cuttingProgress++;
				float cuttingProgressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax;
				OnCuttingProgressChanged?.Invoke(cuttingProgressNormalized);
				OnCut?.Invoke();
				OnAnyCut?.Invoke(transform.position);

				if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
				{
					cuttingProgress = 0;
					GetKitchenObject().DestroySelf();
					KitchenObject.CreateKitchenObject(cuttingRecipeSO.output, this);
				}
			}
		}
	}

	private CuttingRecipeSO FindCuttingRecipeSO(KitchenObjectSO inputKitchenObject)
	{
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeSOs)
		{
			if (cuttingRecipeSO.input == inputKitchenObject)
			{
				return cuttingRecipeSO;
			}
        }

		return null;
    }
}
