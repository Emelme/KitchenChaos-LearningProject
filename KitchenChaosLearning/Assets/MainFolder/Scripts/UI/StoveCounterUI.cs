using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoveCounterUI : MonoBehaviour
{
	[SerializeField] private Image progressBar;


	[SerializeField] private StoveCounter stoveCounter;

	private void Start()
	{
		stoveCounter.OnStartCooking += StoveCounter_OnStartCooking;
		stoveCounter.OnStopAllStoveCoroutine += StoveCounter_OnStopAllCoroutine;

		progressBar.fillAmount = 0;

		Hide();
	}

	private void StoveCounter_OnStartCooking(float cookingTime)
	{
		Show();
		StartCoroutine(StartProgressBarTimer(cookingTime));
	}

	private void StoveCounter_OnStopAllCoroutine()
	{
		StopAllCoroutines();
		Hide();
	}

	private IEnumerator StartProgressBarTimer(float cookingTime)
	{
		float cookingTimer = 0;

		while (cookingTimer <= cookingTime)
		{
			progressBar.fillAmount = cookingTimer / cookingTime;

			cookingTimer += Time.deltaTime;

			yield return null;
		}

		Hide();
	}

	private void Show()
	{
		gameObject.SetActive(true);
	}

	private void Hide()
	{
		gameObject.SetActive(false);
	}
}
