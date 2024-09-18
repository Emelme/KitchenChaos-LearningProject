using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
	[SerializeField] private Counter baseCounter;
	[SerializeField] private GameObject[] selectedCounterVisual;

	private void Start()
	{
		Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
	}

	private void Player_OnSelectedCounterChanged(Counter selectedCounter)
	{
		if (selectedCounter == baseCounter)
		{
			for (int i = 0; i < selectedCounterVisual.Length; i++)
			{
				selectedCounterVisual[i].SetActive(true);
			}
		}
		else
		{
			for (int i = 0; i < selectedCounterVisual.Length; i++)
			{
				selectedCounterVisual[i].SetActive(false);
			}
		}
	}
}
