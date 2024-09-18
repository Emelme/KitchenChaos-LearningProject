using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
	[SerializeField] private Animator animator;
	[SerializeField] private CuttingCounter cuttingCounter;

	private const string CUT = "cut";

	private void Start()
	{
		cuttingCounter.OnCut += CuttingCounter_OnCut;
	}

	private void CuttingCounter_OnCut()
	{
		animator.SetTrigger(CUT);
	}
}
