using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CuttingCounterUI : MonoBehaviour
{
	[SerializeField] private Image progressBar;
	[SerializeField] private CuttingCounter cuttingCounter;

	private void Start()
	{
		cuttingCounter.OnCuttingProgressChanged += CuttingCounter_OnCuttingProgressChanged;

		progressBar.fillAmount = 0;

		Hide();
	}

	private void CuttingCounter_OnCuttingProgressChanged(float cuttingProgressNormalized)
	{
		progressBar.fillAmount = cuttingProgressNormalized;

		if (cuttingProgressNormalized == 0f || progressBar.fillAmount == 1f)
		{
			Hide();
		}
		else
		{
			Show();
		}
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
