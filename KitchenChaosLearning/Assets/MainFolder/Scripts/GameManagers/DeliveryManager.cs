using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DeliveryManager : MonoBehaviour
{
	public static DeliveryManager Instance { get; private set; }

	[SerializeField] private RecipeListSO recipeListSO;

	[SerializeField] private float waitingRecipeSpawnTime;
	[SerializeField] private int maxWaitingRecipeAmount;

	private List<RecipeSO> waitingRecipeSOList;

	private int completedRecipeCount;

	private bool isCoroutineRunning = false;
 
	public event Action<RecipeSO> OnWatingRecipeAdded;
	public event Action<RecipeSO> OnWatingRecipeRemoved;

	public event Action<Vector3> OnDeliverySuccess;
	public event Action<Vector3> OnDeliveryFailed;

	private void Awake()
	{
		Instance = this;

		waitingRecipeSOList = new List<RecipeSO>();

		StartCoroutine(SpawnWaitingRecipe());
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.T))
		{
			RecipeSO recipeSO = waitingRecipeSOList[0];

			MakeDelivery(recipeSO, Vector3.zero);
		}
	}

	private IEnumerator SpawnWaitingRecipe()
	{
		isCoroutineRunning = true;

		yield return new WaitForSeconds(waitingRecipeSpawnTime);

		isCoroutineRunning = false;

		if (GameManager.Instance.IsGamePlaying())
		{
			RecipeSO recipeSO = recipeListSO.recipeSOList[Random.Range(0, recipeListSO.recipeSOList.Count)];
			waitingRecipeSOList.Add(recipeSO);
			OnWatingRecipeAdded?.Invoke(recipeSO); 
		}

		if (GameManager.Instance.IsGamePlaying() && waitingRecipeSOList.Count < maxWaitingRecipeAmount)
		{
			StartCoroutine(SpawnWaitingRecipe()); 
		}
	}

	public void HandleDelivery(PlateKitchenObject plate)
	{
		List<KitchenObjectSO> plateKitchenObjectSOList = plate.GetIngredientList();

		bool areIngredientsMatching = false;

		// Для каждого рецепта в списке заказов
		foreach (RecipeSO recipeSO in waitingRecipeSOList)
		{
			List<KitchenObjectSO> recipeKitchenObjectSOList = recipeSO.kitchenObjectSOList;

			// Если число ингредиентов совпадает
			if (plateKitchenObjectSOList.Count == recipeKitchenObjectSOList.Count)
			{
				// Для каждого ингридиента на тарелке
				foreach (KitchenObjectSO plateKitchneObjectSO in plateKitchenObjectSOList)
				{
					// Для каждого ингредента в рецепте
					foreach (KitchenObjectSO recipeKitchenObjectSO in recipeKitchenObjectSOList)
					{
						// Если ингредиенты совпадают
						if (plateKitchneObjectSO == recipeKitchenObjectSO)
						{
							areIngredientsMatching = true;
							break;
						}
						else
						{
							areIngredientsMatching = false;
						}
					}

					// Если ни однин ингредиент из рецепта не совпал с ингредиентом на тарелке то закончить проверять ингредиенты в этом рецепте
					if (!areIngredientsMatching) break;
				}

				if (areIngredientsMatching)
				{
					MakeDelivery(recipeSO, plate.transform.position);
					return;
				}
			}
		}

		OnDeliveryFailed?.Invoke(plate.transform.position);
	}

	private void MakeDelivery(RecipeSO recipeSO, Vector3 soundSpawnPosition)
	{
		completedRecipeCount++;

		waitingRecipeSOList.Remove(recipeSO);
		OnWatingRecipeRemoved?.Invoke(recipeSO);

		if (!isCoroutineRunning)
		{
			StartCoroutine(SpawnWaitingRecipe());
		}
				
		OnDeliverySuccess?.Invoke(soundSpawnPosition);
	}

	public List<RecipeSO> GetWaitingRecipeSOList() { return waitingRecipeSOList; }

	public int GetCompletedRecipeCount() { return completedRecipeCount; }
}
