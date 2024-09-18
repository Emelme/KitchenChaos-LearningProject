using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
	[SerializeField] private Button playButton;
	[SerializeField] private Button quitButton;

	[SerializeField] private GameObject loadingSceneUI;

	private void Awake()
	{
		Time.timeScale = 1f;

		loadingSceneUI.SetActive(false);

		playButton.onClick.AddListener(() =>
		{
			loadingSceneUI.SetActive(true);

			Loader.LoadScene(Loader.Scene.MainScene);
		});

		quitButton.onClick.AddListener(() =>
		{
			Application.Quit();
		});
	}
}
