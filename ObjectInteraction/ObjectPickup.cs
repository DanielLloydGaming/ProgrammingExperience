using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectPickup : MonoBehaviour
{
	[Header("GameObject Transforms")]
	[Space(5)]
	[SerializeField] private Transform playerCameraTransform;
	[SerializeField] private Transform objectHeldTransform;
	[SerializeField] private Transform highlightedObject;
	//[SerializeField] private NoteController noteController;
	[SerializeField] private ObjectGrabbable objectGrabbable;

	[Space(10)]
	[Header("Choose the Interaction Key")]
	[Space(5)]
	[SerializeField] private InputActionReference pickUpAction;

	[Space(10)]
	[Header("Select layer you want to affect")]
	[Space(5)]
	[SerializeField] private LayerMask pickUpLayerMask;

	[Space(10)]
	[Header("Choose a Pickup Distance")]
	[Space(5)]
	[Range(1f,5f)]
	[SerializeField] private float pickUpDistance = 2.0f;


 //   private void OnEnable()
 //   {
	//	pickUpAction.action.performed += PickUpDrop;
	//}

 //   private void OnDisable()
 //   {
	//	pickUpAction.action.performed -= PickUpDrop;
	//}

 //   private void Start()
 //   {
	//	playerCameraTransform = GetComponent<Camera>().transform;
	//}

 //   private void PickUpDrop(InputAction.CallbackContext obj)
	//{
	//	if(objectGrabbable == null)
 //       {
	//		if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickUpDistance, pickUpLayerMask))
	//		{
	//			if (raycastHit.transform.TryGetComponent(out objectGrabbable))
	//			{
	//				objectGrabbable.Grab(objectHeldTransform);
	//			}
	//		}
	//	}
	//	else if(noteController == null && objectGrabbable == null)
 //       {
	//		if(Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickUpDistance, pickUpLayerMask))
 //           {
	//			Debug.Log(raycastHit.transform.name + " " + "has been selected for the note controller.");
	//			if(raycastHit.transform.TryGetComponent(out noteController))
 //               {
	//				Debug.Log("Selected note controller.");
	//				//noteController.ReadNote();
	//				Debug.Log("Read the note.");
 //               }
 //           }
 //       }
 //       else
 //       {
	//		objectGrabbable.Drop();
	//		noteController.CloseNote(obj);
	//		objectGrabbable = null;
	//		noteController = null;
 //       }
	//}
}
