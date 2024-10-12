using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI moveUpKeyText;
	[SerializeField] private TextMeshProUGUI moveDownKeyText;
	[SerializeField] private TextMeshProUGUI moveLeftKeyText;
	[SerializeField] private TextMeshProUGUI moveRightKeyText;
	[SerializeField] private TextMeshProUGUI interactKeyText;
	[SerializeField] private TextMeshProUGUI altInteractText;
	[SerializeField] private TextMeshProUGUI pauseText;

	private void Start()
	{
		GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
		GameInput.Instance.OnBindingRebind += GameInput_OnBindingRebind;

		UpdateVisual();
	}

	private void GameInput_OnInteractAction()
	{
		gameObject.SetActive(false);
	}

	private void GameInput_OnBindingRebind()
	{
		UpdateVisual();
	}

	private void UpdateVisual()
	{
		moveUpKeyText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveUp);
		moveDownKeyText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveDown);
		moveLeftKeyText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveLeft);
		moveRightKeyText.text = GameInput.Instance.GetBindingText(GameInput.Binding.MoveRight);
		interactKeyText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
		altInteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.InteractAlt);
		pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
	}
}
