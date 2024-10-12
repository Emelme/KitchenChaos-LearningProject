using System;
using UnityEngine;
using Unity.Netcode;

public class Player : NetworkBehaviour, IKitchenObjectParent
{
	public static Player Instance { get; private set; }

	[SerializeField] private float moveSpeed;
	[SerializeField] private Transform kitchenObjectHoldPoint;

	public bool IsWalking { get; private set; }

	private Vector3 lastMoveDirection;
	[SerializeField] private LayerMask countersLayerMask;

	private Counter selectedCounter;
	private KitchenObject kitchenObject;

	public event Action<Counter> OnSelectedCounterChanged;

	public event Action OnObjectPicked;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
		GameInput.Instance.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
	}

	private void GameInput_OnInteractAction()
	{
		if (!GameManager.Instance.IsGamePlaying()) { return; }

		if (selectedCounter != null)
		{
			selectedCounter.Interact(); 
		}
	}

	private void GameInput_OnInteractAlternateAction()
	{
		if (!GameManager.Instance.IsGamePlaying()) { return; }

		if (selectedCounter != null)
		{
			selectedCounter.InteractAlternate();
		}
	}

	private void Update()
	{
		if (!IsOwner) { return; }

		HandleMovement();
		HandleInteraction();
	}

	private void HandleMovement()
	{
		Vector2 inputVector = GameInput.Instance.GetInputVector2Normalized();
		Vector3 moveDirection = new(inputVector.x, 0f, inputVector.y);

		IsWalking = moveDirection != Vector3.zero;

		if (IsWalking)
		{
			lastMoveDirection = moveDirection;
		}

		float playerHeight = 2f;
		float playerRadius = 0.7f;
		float moveDistance = moveSpeed * Time.deltaTime;

		bool canMove;
		canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirection, moveDistance);

		// checking if you can move along a wall
		if (!canMove)
		{
			Vector3 moveDirectionX = new(moveDirection.x, 0f, 0f);
			canMove = moveDirection.x != 0f && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionX, moveDistance);

			if (canMove)
			{
				moveDirection = moveDirectionX.normalized;
			}
			else
			{
				Vector3 moveDirectionZ = new(0f, 0f, moveDirection.z);
				canMove = moveDirection.z != 0f && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionZ, moveDistance);

				if (canMove)
				{
					moveDirection = moveDirectionZ.normalized;
				}
				else
				{
					canMove = false;
				}
			}
		}

		if (canMove)
		{
			transform.position += moveDirection * moveDistance;
		}

		float rotateSpeed = 10f;
		transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);
	}

	private void HandleInteraction()
	{
		float interactDistance = 2f;

		if (Physics.Raycast(transform.position, lastMoveDirection, out RaycastHit hitInfo, interactDistance, countersLayerMask))
		{
			if (hitInfo.transform.TryGetComponent(out Counter baseCounter))
			{
				if (selectedCounter != baseCounter)
				{
					ChangeSelectedCounter(baseCounter);
				}
			}
			else
			{
				ChangeSelectedCounter(null);
			}
		}
		else
		{
			ChangeSelectedCounter(null);
		}
	}

	private void ChangeSelectedCounter(Counter selectedCounter)
	{
		this.selectedCounter = selectedCounter;

		OnSelectedCounterChanged?.Invoke(selectedCounter);
	}

	public KitchenObject GetKitchenObject() { return kitchenObject; }

	public void SetKitchenObject(KitchenObject kitchenObject)
	{
		this.kitchenObject = kitchenObject;

		if (kitchenObject != null) { OnObjectPicked?.Invoke(); }
	}

	public void ClearKitchenObject() { kitchenObject = null; }

	public bool HasKitchenObject() { return kitchenObject != null; }

	public Transform GetKitchenObjectHoldPoint() { return kitchenObjectHoldPoint; }
}
