using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
	[SerializeField] private GameObject recipeTempaltesContainer;
	[SerializeField] private GameObject recipeTemplate;

	private List<RecipeTemplateUI> recipeTemplateUIList;

	private void Awake()
	{
		recipeTemplate.SetActive(false);
		recipeTemplateUIList = new List<RecipeTemplateUI>();
	}

	private void Start()
	{
		DeliveryManager.Instance.OnWatingRecipeAdded += DeliveryManager_OnWatingRecipeAdded;
		DeliveryManager.Instance.OnWatingRecipeRemoved += DeliveryManager_OnWatingRecipeRemoved;
	}

	private void DeliveryManager_OnWatingRecipeAdded(RecipeSO recipeSO)
	{
		AddRecipeTemplate(recipeSO);
	}

	private void DeliveryManager_OnWatingRecipeRemoved(RecipeSO recipeSO)
	{
		RemoveRecipeTemplate(recipeSO);
	}

	private void AddRecipeTemplate(RecipeSO recipeSO)
	{
		GameObject recipeTemplateGameObject = Instantiate(recipeTemplate, recipeTempaltesContainer.transform);
		recipeTemplateGameObject.SetActive(true);
		RecipeTemplateUI recipeTemplateUI = recipeTemplateGameObject.GetComponent<RecipeTemplateUI>();
		recipeTemplateUIList.Add(recipeTemplateUI);
		recipeTemplateUI.SetRecipeSO(recipeSO);
	}

	private void RemoveRecipeTemplate(RecipeSO recipeSO)
	{
		foreach (RecipeTemplateUI recipeTemplateUI in recipeTemplateUIList)
		{
			if (recipeTemplateUI.GetRecipeSO() == recipeSO)
			{
				recipeTemplateUIList.Remove(recipeTemplateUI);
				Destroy(recipeTemplateUI.gameObject);
				return;
			}
		}
	}
}
