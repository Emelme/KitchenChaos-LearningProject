using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI coundownText;

	private void Start()
	{
		GameManager.Instance.OnCountdownToStartStarted += GameManager_OnCountdownToStartStarted;

		gameObject.SetActive(false);
	}

	private void GameManager_OnCountdownToStartStarted()
	{
		gameObject.SetActive(true);
		StartCoroutine(StartCountdown());
	}

	private IEnumerator StartCountdown()
	{
		float timer = GameManager.Instance.GetCountdownTime();

		while (true)
		{
			timer -= Time.deltaTime;

			if (timer > 0f)
			{
				coundownText.text = timer.ToString("F2");

				yield return null; 
			}
			else
			{
				coundownText.text = "0,00";
				break;
			}
		}

		gameObject.SetActive(false);
	}
}
