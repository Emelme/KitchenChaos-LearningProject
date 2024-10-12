using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
	[SerializeField] private Button soundButton;
	[SerializeField] private Button musicButton;
	[SerializeField] private Button closeButton;

	[SerializeField] private TextMeshProUGUI soundButtonText;
	[SerializeField] private TextMeshProUGUI musicButtonText;

	[Space]
	[SerializeField] private Button moveUpButton;
	[SerializeField] private Button moveDownButton;
	[SerializeField] private Button moveLeftButton;
	[SerializeField] private Button moveRightButton;
	[SerializeField] private Button interactButton;
	[SerializeField] private Button interactAltButton;
	[SerializeField] private Button pauseButton;

	[SerializeField] private TextMeshProUGUI moveUpButtonText;
	[SerializeField] private TextMeshProUGUI moveDownButtonText;
	[SerializeField] private TextMeshProUGUI moveLeftButtonText;
	[SerializeField] private TextMeshProUGUI moveRightButtonText;
	[SerializeField] private TextMeshProUGUI interactButtonText;
	[SerializeField] private TextMeshProUGUI interactAltButtonText;
	[SerializeField] private TextMeshProUGUI pauseButtonText;

	[SerializeField] private GameObject pressToRebindKeyWindow;

	private void Awake()
	{
		soundButton.onClick.AddListener(() =>
		{
			SoundManager.Instance.ChangeVolumeMultiplier();
			soundButtonText.text = "Sound: " + Mathf.Round(SoundManager.Instance.GetVolumeMultiplier() * 10f);
		});

		musicButton.onClick.AddListener(() =>
		{
			MusicManager.Instance.ChangeVolumeMultiplier();
			musicButtonText.text = "Music: " + MusicManager.Instance.GetVolumeMultiplierNormalized();
		});

		closeButton.onClick.AddListener(() =>
		{
			GameManager.Instance.TogglePause();
			Hide();
		});

		moveUpButton.onClick.AddListener(() => { Rebind(GameInput.Binding.MoveUp); });

		moveDownButton.onClick.AddListener(() => { Rebind(GameInput.Binding.MoveDown); });

		moveLeftButton.onClick.AddListener(() => { Rebind(GameInput.Binding.MoveLeft); });

		moveRightButton.onClick.AddListener(() => { Rebind(GameInput.Binding.MoveRight); });

		interactButton.onClick.AddListener(() => { Rebind(GameInput.Binding.Interact); });

		interactAltButton.onClick.AddListener(() => { Rebind(GameInput.Binding.InteractAlt); });

		pauseButton.onClick.AddListener(() => { Rebind(GameInput.Binding.Pause); });

		HidePressToRebindKey();
		Hide();
	}

	private void Start()
	{
		GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;

		UpdateVisual();
	}

	private void GameManager_OnGameUnpaused()
	{
		Hide();
	}

	private void UpdateVisual()
	{
		soundButtonText.text = "Sound: " + Mathf.Round(SoundManager.Instance.GetVolumeMultiplier() * 10f);
		musicButtonText.text = "Music: " + MusicManager.Instance.GetVolumeMultiplierNormalized();

		moveUpButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveUp);
		moveDownButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveDown);
		moveLeftButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveLeft);
		moveRightButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveRight);
		interactButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
		interactAltButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlt);
		pauseButtonText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
	}

	private void Rebind(GameInput.Binding binding)
	{
		ShowPressToRebindKey();

		GameInput.Instance.RebindBinding(binding, () =>
		{
			HidePressToRebindKey();
			UpdateVisual();
		});
	}

	private void ShowPressToRebindKey()
	{
		pressToRebindKeyWindow.SetActive(true);
	}

	private void HidePressToRebindKey()
	{
		pressToRebindKeyWindow.SetActive(false);
	}

	public void Show()
	{
		gameObject.SetActive(true);
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}


}
