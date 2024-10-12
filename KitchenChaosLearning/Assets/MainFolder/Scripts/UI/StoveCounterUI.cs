using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StoveCounterUI : MonoBehaviour
{
	[SerializeField] private StoveCounter stoveCounter;

	[SerializeField] private Image progressBar;
	[SerializeField] private Color progressBarCookingColor;
	[SerializeField] private Color progressBarBurningColor;

	[SerializeField] private GameObject warningUI;

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
		HideWarningUI();
		StartCoroutine(StartProgressBarTimer(cookingTime));

		if (stoveCounter.IsBurning())
		{
			progressBar.color = progressBarBurningColor;
		}
		else
		{
			progressBar.color = progressBarCookingColor;
		}
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

			if (cookingTime - cookingTimer < 1f && stoveCounter.IsBurning())
			{
				ShowWarningUI();
			}

			yield return null;
		}

		Hide();
	}

	private void ShowWarningUI()
	{
		warningUI.SetActive(true);
	}

	private void HideWarningUI()
	{
		warningUI.SetActive(false);
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
