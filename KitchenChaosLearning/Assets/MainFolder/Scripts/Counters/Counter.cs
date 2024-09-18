using System;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Counter : MonoBehaviour, IKitchenObjectParent
{
	[SerializeField] private Transform kitchenObjectHoldPoint;

	private KitchenObject kitchenObject;

	public static event Action<Vector3> OnObjectDropped;

	public static void ResetStaticData()
	{
		OnObjectDropped = null;
	}

	public abstract void Interact();

	public virtual void InteractAlternate() { }

	public KitchenObject GetKitchenObject() { return kitchenObject; }

	public void SetKitchenObject(KitchenObject kitchenObject)
	{
		this.kitchenObject = kitchenObject;
		OnObjectDropped?.Invoke(transform.position);
	}

	public void ClearKitchenObject() { kitchenObject = null; }

	public bool HasKitchenObject() { return kitchenObject != null; }

	public Transform GetKitchenObjectHoldPoint() { return kitchenObjectHoldPoint; }
}
