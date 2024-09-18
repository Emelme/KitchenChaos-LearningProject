using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class RecipeSO : ScriptableObject
{
	public new string name;

	public float timeToCompleteRecipe;

	public List<KitchenObjectSO> kitchenObjectSOList;
}
