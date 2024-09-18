using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RecipeTemplateUI : MonoBehaviour
{
	private RecipeSO recipeSO;

	[SerializeField] private GameObject iconsContainer;
	[SerializeField] private GameObject iconTemplate;
	[SerializeField] private TextMeshProUGUI recipeTemplateText;

	private void Awake()
	{
		iconTemplate.SetActive(false);
	}

	public void SetRecipeSO(RecipeSO recipeSO)
	{
		this.recipeSO = recipeSO;
		UpdateVisual();
	}

	public RecipeSO GetRecipeSO() { return recipeSO; }

	public void UpdateVisual()
	{
		recipeTemplateText.text = recipeSO.name;

		foreach (Transform childTransform in iconsContainer.transform)
		{
			if (childTransform.gameObject == iconTemplate) continue;
			Destroy(childTransform.gameObject);
		}

		foreach (KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectSOList)
		{
			GameObject iconTemplateGameObject = Instantiate(iconTemplate, iconsContainer.transform);
			iconTemplateGameObject.SetActive(true);

			IconTemplateUI iconImageUI = iconTemplateGameObject.GetComponent<IconTemplateUI>();
			iconImageUI.SetIconSprite(kitchenObjectSO.sprite);
		}
	}
}
