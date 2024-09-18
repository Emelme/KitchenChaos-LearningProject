using UnityEngine;

public class ResetStaticDataManager : MonoBehaviour
{
	private void Awake()
	{
		CuttingCounter.ResetStaticData();
		Counter.ResetStaticData();
		TrashCounter.ResetStaticData();
		RecipeCountdownUI.ResetStaticData();
	}
}
