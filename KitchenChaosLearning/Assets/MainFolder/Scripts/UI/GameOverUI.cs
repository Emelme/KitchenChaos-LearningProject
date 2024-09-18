using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI recipesDeliveredText;

	private void Start()
	{
		RecipeCountdownUI.OnRecipeCountdownExpired += RecipeCountdownUI_OnRecipeCountdownExpired;

		gameObject.SetActive(false);
	}

	private void RecipeCountdownUI_OnRecipeCountdownExpired()
	{
		gameObject.SetActive(true);

		recipesDeliveredText.text = DeliveryManager.Instance.GetCompletedRecipeCount().ToString();
	}
}
