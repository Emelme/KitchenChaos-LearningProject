using UnityEngine;

public class KitchenObject : MonoBehaviour
{
	[SerializeField] private KitchenObjectSO kitchenObjectSO;

	private IKitchenObjectParent kitchenObjectParent;

	public IKitchenObjectParent GetKitchenObjectParent() { return kitchenObjectParent; }

	public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent)
	{
		if (this.kitchenObjectParent != null)
		{
			this.kitchenObjectParent.ClearKitchenObject();
		}

		this.kitchenObjectParent = kitchenObjectParent;
		 
		if (kitchenObjectParent.HasKitchenObject())
		{
			Debug.LogError("IKitchenObject already has KitchenObject");
		}

		kitchenObjectParent.SetKitchenObject(this);

		transform.parent = kitchenObjectParent.GetKitchenObjectHoldPoint();
		transform.localPosition = Vector3.zero;
	}

	public KitchenObjectSO GetKitchenObjectSO() { return kitchenObjectSO; }

	public static KitchenObject CreateKitchenObject(KitchenObjectSO kitchenObjectSO, IKitchenObjectParent kitchenObjectParent)
	{
		GameObject kitchenObjectInstance = Instantiate(kitchenObjectSO.kitchenObjectPrefab);
		KitchenObject kitchenObject = kitchenObjectInstance.GetComponent<KitchenObject>();
		kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
		return kitchenObject;		
	}

	public void DestroySelf()
	{
		kitchenObjectParent.ClearKitchenObject();

		Destroy(gameObject);
	}

	public bool TryGetPlate(out PlateKitchenObject plate)
	{
		if (this is PlateKitchenObject)
		{
			plate = this as PlateKitchenObject;
			return true;
		}
		else
		{
			plate = null;
			return false;
		}
	}
}
