using System;
using System.Collections;
using UnityEngine;

public class StoveCounter : Counter
{
	[SerializeField] private FryingRecipeSO[] fryingRecipeSOs;
	[SerializeField] private BurningRecipeSO[] burningRecipeSOs;

	private FryingRecipeSO fryingRecipeSO;
	private BurningRecipeSO burningRecipeSO;

	private bool isBurning = false;

	public event Action OnInteracted;
	public event Action<float> OnStartCooking;
	public event Action OnStopAllStoveCoroutine;

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
						isBurning = false;
						StopAllCoroutines();
						OnStopAllStoveCoroutine?.Invoke();
					}
				}
			}
			else
			{
				fryingRecipeSO = FindFryingRecipeSO(Player.Instance.GetKitchenObject().GetKitchenObjectSO());
				burningRecipeSO = FindBurningRecipeSO(Player.Instance.GetKitchenObject().GetKitchenObjectSO());

				if (fryingRecipeSO != null)
				{
					Player.Instance.GetKitchenObject().SetKitchenObjectParent(this);
					isBurning = false;
					StartCoroutine(StartCooking(fryingRecipeSO.FryingTime, fryingRecipeSO.output));
				}
				else if (burningRecipeSO != null)
				{
					Player.Instance.GetKitchenObject().SetKitchenObjectParent(this);
					isBurning = true;
					StartCoroutine(StartCooking(burningRecipeSO.burningTime, burningRecipeSO.output));
				} 
			}
		}
		else
		{
			if (HasKitchenObject())
			{
				isBurning = false;
				StopAllCoroutines();
				OnStopAllStoveCoroutine?.Invoke();
				GetKitchenObject().SetKitchenObjectParent(Player.Instance); 
			}
		}

		OnInteracted?.Invoke();
	}

	private IEnumerator StartCooking(float cookingTime, KitchenObjectSO outputKitchenObjectSO)
	{
		OnStartCooking?.Invoke(cookingTime);

		yield return new WaitForSeconds(cookingTime);

		GetKitchenObject().DestroySelf();
		KitchenObject.CreateKitchenObject(outputKitchenObjectSO, this);

		burningRecipeSO = FindBurningRecipeSO(GetKitchenObject().GetKitchenObjectSO());

		if (burningRecipeSO != null)
		{
			isBurning = true;
			StartCoroutine(StartCooking(burningRecipeSO.burningTime, burningRecipeSO.output));
		}
	}

	private FryingRecipeSO FindFryingRecipeSO(KitchenObjectSO inputKitchenObject)
	{
		foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOs)
		{
			if (fryingRecipeSO.input == inputKitchenObject)
			{
				return fryingRecipeSO;
			}
		}

		return null;
	}

	private BurningRecipeSO FindBurningRecipeSO(KitchenObjectSO inputKitchenObject)
	{
		foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOs)
		{
			if (burningRecipeSO.input == inputKitchenObject)
			{
				return burningRecipeSO;
			}
		}

		return null;
	}

	public bool IsBurning() { return isBurning; }
}
