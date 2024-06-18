using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour, IInteractable, IInitializeable, IRaycastSight, IReadable, IDebuggable
{
    #region GameObject Information
    [Header("GameObject Transforms")]
	[Space(5)]
	[SerializeField] private Transform playerCameraTransform;
	[SerializeField] private Transform objectHeldTransform;
	[SerializeField] private NoteController noteController;
	[SerializeField] private ObjectGrabbable objectGrabbable;
	[SerializeField] public static bool isItemHeld = false;
    #endregion

    #region Designer Options
    [Space(10)]
	[Header("Choose the Interaction Key")]
	[Space(5)]
	[SerializeField] private InputActionReference interactionAction;
	[Header("Choose the Reading Key")]
	[Space(5)]
	[SerializeField] private InputActionReference readingAction;

	[Header("Choose the Debug Key")]
	[Space(5)]
	[SerializeField] private InputActionReference debugAction;

	[Space(10)]
	[Header("Select layer you want to affect")]
	[Space(5)]
	[SerializeField] private LayerMask pickUpLayerMask;

	[Space(10)]
	[Header("Choose a Pickup Distance")]
	[Space(5)]
	[Range(1f, 5f)]
	[SerializeField] private float pickUpDistance = 2.0f;
	#endregion

	public delegate void PlayerHoldingItem(bool isItemHeld);
	public static PlayerHoldingItem onPlayerHoldingItem;

    private void OnEnable()
	{
		interactionAction.action.performed += Interaction;
		readingAction.action.performed += Reading;
		onPlayerHoldingItem += HoldingItem;
		debugAction.action.performed += DebugAction;
	}

	private void OnDisable()
	{
		interactionAction.action.performed -= Interaction;
		readingAction.action.performed -= Reading;
		onPlayerHoldingItem -= HoldingItem;
		debugAction.action.performed -= DebugAction;
	}

	private void Start()
	{
		Initialize();
	}

    private void Interaction(InputAction.CallbackContext obj)
	{
		StartInteraction();
	}
    public void Initialize()
    {
		playerCameraTransform = GetComponent<Camera>().transform;
		isItemHeld = false;
	}

    public void StartInteraction()
    {
		if (objectGrabbable == null)
		{
			Raycast();
		}
		else
		{
			objectGrabbable.Drop();
			objectGrabbable = null;
		}
	}

    public void StopInteraction()
    {
        
    }

	public void Reading(InputAction.CallbackContext obj)
    {
		ReadItem();
    }

    public void Raycast()
    {
		if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickUpDistance, pickUpLayerMask))
		{
			if (raycastHit.transform.TryGetComponent(out objectGrabbable))
			{
				objectGrabbable.Grab(objectHeldTransform);
			}
		}
	}

    public void ReadItem()
    {
		if (noteController == null)
        {
            if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickUpDistance, pickUpLayerMask))
            {
                Debug.Log(raycastHit.transform.name + " " + "has been selected for the note controller.");
                if (raycastHit.transform.TryGetComponent(out noteController))
                {
					noteController.StartInteraction();
                }
            }
        }
        else
        {
            noteController = null;
		}
    }

	public void HoldingItem(bool isHolding)
    {
		isItemHeld = isHolding;
    }


    #region Debugging Only
    private void DebugAction(InputAction.CallbackContext debugAction)
    {
		DebugThisForMe();
    }

    public void DebugThisForMe()
    {
		foreach (var item in KeepBoxManager.keptItems)
		{
			Debug.Log("Items in the KEEP BOX:");
			Debug.Log(item);
		}
		foreach (var item in ThrowBoxManager.thrownItems)
		{
			Debug.Log("Items in the THROW BOX:");
			Debug.Log(item);
		}
	}
	#endregion
}
