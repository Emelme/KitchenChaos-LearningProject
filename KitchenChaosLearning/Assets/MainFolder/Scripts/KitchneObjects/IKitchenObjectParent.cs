using UnityEngine;

public interface IKitchenObjectParent
{
	public KitchenObject GetKitchenObject();

	public void SetKitchenObject(KitchenObject kitchenObject);

	public void ClearKitchenObject();

	public bool HasKitchenObject();

	public Transform GetKitchenObjectHoldPoint();
}
