using System;
using UnityEngine;
using UnityEngine.UI;

public class RecipeCountdownUI : MonoBehaviour
{
	[SerializeField] private RecipeTemplateUI recipeTemplateUI;
	[SerializeField] private Image recipeCountdownClockImage;

	private float timeToCompleteRecipe;
	private float recipeCountdownTimer;

	public static event Action OnRecipeCountdownExpired;

	public static void ResetStaticData()
	{
		OnRecipeCountdownExpired = null;
	}

	private void Start()
	{
		timeToCompleteRecipe = recipeTemplateUI.GetRecipeSO().timeToCompleteRecipe;
		recipeCountdownTimer = timeToCompleteRecipe;		
	}

	private void Update()
	{
		recipeCountdownTimer -= Time.deltaTime;

		if (recipeCountdownTimer <= 0f)
		{
			OnRecipeCountdownExpired?.Invoke();
			recipeCountdownClockImage.fillAmount = 0f;
		}
		else
		{
			recipeCountdownClockImage.fillAmount = recipeCountdownTimer / timeToCompleteRecipe;
		}
	}
}
