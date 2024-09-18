using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : Counter
{
	public static event Action<Vector3> OnObjectTrashed;

	new public static void ResetStaticData()
	{
		OnObjectTrashed = null;
	}

	public override void Interact()
	{
		if(Player.Instance.HasKitchenObject())
		{
			Player.Instance.GetKitchenObject().DestroySelf();
			OnObjectTrashed?.Invoke(transform.position);
		}
	}
}
