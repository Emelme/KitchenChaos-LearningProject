using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
	[SerializeField] private Animator animator;
	[SerializeField] private ContainerCounter containerCounter;

	private const string OPEN_CLOSE = "openClose";

	private void Start()
	{
		containerCounter.OnContainerCounterInteracted += ContainerCounter_OnContainerCounterInteracted;
	}

	private void ContainerCounter_OnContainerCounterInteracted()
	{
		animator.SetTrigger(OPEN_CLOSE);
	}
}
