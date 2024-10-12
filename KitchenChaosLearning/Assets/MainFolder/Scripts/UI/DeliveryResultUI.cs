using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour
{
	[SerializeField] private Image backgroundImage;
	[SerializeField] private Color successColor;
	[SerializeField] private Color failedColor;
	[SerializeField] private TextMeshProUGUI messageText;
	[SerializeField] private Animator animator;
	private const string POPUP_TRIGGER = "popupTrigger";

	private void Start()
	{
		DeliveryManager.Instance.OnDeliverySuccess += DeliveryManager_OnDeliverySuccess;
		DeliveryManager.Instance.OnDeliveryFailed += DeliveryManager_OnDeliveryFailed;

		gameObject.SetActive(false);
	}

	private void DeliveryManager_OnDeliverySuccess(Vector3 obj)
	{
		backgroundImage.color = successColor;
		messageText.text = "DELIVERY\nSUCCESS";
		animator.SetTrigger(POPUP_TRIGGER);

		gameObject.SetActive(true);
	}

	private void DeliveryManager_OnDeliveryFailed(Vector3 obj)
	{
		backgroundImage.color= failedColor;
		messageText.text = "DELIVERY\nFAILED";
		animator.SetTrigger(POPUP_TRIGGER);

		gameObject.SetActive(true);
	}
}
