using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance { get; private set; }

	private float countdownToStartTime = 3f;

	private enum State
	{
		WaitingToStart,
		CountdownToStart,
		GamePlaying,
		GameOver
	}

	private State state;

	private bool isGamePaused;

	public event Action OnCountdownToStartStarted;

	public event Action OnGamePaused;
	public event Action OnGameUnpaused;

	private void Awake()
	{
		Instance = this;

		state = State.WaitingToStart;
	}

	private void Start()
	{
		RecipeCountdownUI.OnRecipeCountdownExpired += RecipeCountdownUI_OnRecipeCountdownExpired;
		GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
		GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
	}

	private void GameInput_OnInteractAction()
	{
        if (state == State.WaitingToStart)
        {
			state = State.CountdownToStart;
			OnCountdownToStartStarted?.Invoke();
		}
    }

	private void GameInput_OnPauseAction()
	{
		TogglePause();
	}

	public void TogglePause()
	{
		if (isGamePaused)
		{
			Time.timeScale = 1f;
			OnGameUnpaused?.Invoke();
		}
		else
		{
			Time.timeScale = 0f;
			OnGamePaused?.Invoke();
		}

		isGamePaused = !isGamePaused;
	}

	private void RecipeCountdownUI_OnRecipeCountdownExpired()
	{
		state = State.GameOver;
	}

	private void Update()
	{
		switch (state)
		{
			case State.WaitingToStart:
				break;
			case State.CountdownToStart:
				
				countdownToStartTime -= Time.deltaTime;

				if (countdownToStartTime <= 0f)
				{
					state = State.GamePlaying;
				}

				break;
			case State.GamePlaying:
				break;
			case State.GameOver:
				break;
		}
	}

	public bool IsGamePlaying() { return state == State.GamePlaying; }

	public float GetCountdownTime() { return countdownToStartTime; }
}
