using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI countdownText;
	[SerializeField] private Animator animator;

	private const string COUNTDOWN_POPUP = "countdownPopup";

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
		int previousCountdownNumber = 0;

		while (true)
		{
			timer -= Time.deltaTime;
			int countdownNumber = Mathf.CeilToInt(timer);

			if (timer > 0f)
			{
				countdownText.text = countdownNumber.ToString();

				if (countdownNumber != previousCountdownNumber)
				{
					animator.SetTrigger(COUNTDOWN_POPUP);
					SoundManager.Instance.PlayCountdownSound();
					previousCountdownNumber = countdownNumber;
				}

				yield return null;
			}
			else
			{
				break;
			}
		}

		gameObject.SetActive(false);
	}
}
