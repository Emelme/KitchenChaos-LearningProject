using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
	public static GameInput Instance { get; private set; }

	private PlayerInputAction playerInputAction;

	private const string PLAYER_PREFS_BINDINGS = "InputBindings";

	public event Action OnInteractAction;
	public event Action OnInteractAlternateAction;
	public event Action OnPauseAction;
	public event Action OnBindingRebind;

	public enum Binding
	{
		MoveUp,
		MoveDown,
		MoveLeft,
		MoveRight,
		Interact,
		InteractAlt,
		Pause
	}

	private void Awake()
	{
		Instance = this;

		playerInputAction = new PlayerInputAction();

		if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
		{
			playerInputAction.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
		}

		playerInputAction.Enable();

		playerInputAction.Player.Interact.performed += Interact_performed;
		playerInputAction.Player.InteractAltarnate.performed += InteractAltarnate_performed;
		playerInputAction.Player.Pause.performed += Pause_performed;
	}

	private void Pause_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	{
		OnPauseAction?.Invoke();
	}

	private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	{
		OnInteractAction?.Invoke();
	}

	private void InteractAltarnate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	{
		OnInteractAlternateAction?.Invoke();
	}

	public Vector2 GetInputVector2Normalized()
	{
		return playerInputAction.Player.Move.ReadValue<Vector2>().normalized;
	}

	public string GetBindingText(Binding binding)
	{
		switch (binding)
		{
			default: return null;
			case Binding.MoveUp:
				return playerInputAction.Player.Move.bindings[1].ToDisplayString();
			case Binding.MoveDown:
				return playerInputAction.Player.Move.bindings[2].ToDisplayString();
			case Binding.MoveLeft:
				return playerInputAction.Player.Move.bindings[3].ToDisplayString();
			case Binding.MoveRight:
				return playerInputAction.Player.Move.bindings[4].ToDisplayString();
			case Binding.Interact:
				return playerInputAction.Player.Interact.bindings[0].ToDisplayString();
			case Binding.InteractAlt:
				return playerInputAction.Player.InteractAltarnate.bindings[0].ToDisplayString();
			case Binding.Pause:
				return playerInputAction.Player.Pause.bindings[0].ToDisplayString();
		}
	}

	public void RebindBinding(Binding binding, Action onActionRebound)
	{
		playerInputAction.Player.Disable();

		InputAction inputAction;
		int bindingIndex;

		switch (binding)
		{
			default:
			case Binding.MoveUp:
				inputAction = playerInputAction.Player.Move;
				bindingIndex = 1;
				break;
			case Binding.MoveDown:
				inputAction = playerInputAction.Player.Move;
				bindingIndex = 2;
				break;
			case Binding.MoveLeft:
				inputAction = playerInputAction.Player.Move;
				bindingIndex = 3;
				break;
			case Binding.MoveRight:
				inputAction = playerInputAction.Player.Move;
				bindingIndex = 4;
				break;
			case Binding.Interact:
				inputAction = playerInputAction.Player.Interact;
				bindingIndex = 0;
				break;
			case Binding.InteractAlt:
				inputAction = playerInputAction.Player.InteractAltarnate;
				bindingIndex = 0;
				break;
			case Binding.Pause:
				inputAction = playerInputAction.Player.Pause;
				bindingIndex = 0;
				break;
		}

		inputAction.PerformInteractiveRebinding(bindingIndex)
		.OnComplete(callback =>
		{
			playerInputAction.Player.Enable();
			onActionRebound.Invoke();
			OnBindingRebind?.Invoke();
			PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS, playerInputAction.SaveBindingOverridesAsJson());
			PlayerPrefs.Save();
		})
		.Start();
	}

	private void OnDestroy()
	{
		playerInputAction.Player.Interact.performed -= Interact_performed;
		playerInputAction.Player.InteractAltarnate.performed -= InteractAltarnate_performed;
		playerInputAction.Player.Pause.performed -= Pause_performed;

		playerInputAction.Dispose();
	}
}
