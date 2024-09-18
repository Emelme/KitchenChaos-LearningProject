using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
	[SerializeField] private Button resumeButton;
	[SerializeField] private Button optionsButton;
	[SerializeField] private Button mainMenuButton;

	[SerializeField] OptionsUI optionsUIMenu;

	private void Start()
	{
		GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
		GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;

		resumeButton.onClick.AddListener(() =>
		{
			GameManager.Instance.TogglePause();
		});

		optionsButton.onClick.AddListener(() =>
		{
			optionsUIMenu.Show();
			Hide();
		});

		mainMenuButton.onClick.AddListener(() =>
		{
			Loader.LoadScene(Loader.Scene.MainMenuScene);
		});

		Hide();
	}

	private void GameManager_OnGamePaused()
	{
		Show();
	}

	private void GameManager_OnGameUnpaused()
	{
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
