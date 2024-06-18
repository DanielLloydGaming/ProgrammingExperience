using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ThrowBoxManager : MonoBehaviour, IInitializeable, IThrowawayable
{
    [SerializeField] public static List<string> thrownItems = new List<string>();
    [SerializeField] public TextMeshProUGUI textToChange;
    [SerializeField] private string itemName;
    [SerializeField] private string itemDescription;
    [SerializeField] private string itemThrownText;
    [SerializeField] private BoxCollider colliderState;
    [SerializeField] private BoxListSO throwBoxSO;

    private void OnEnable()
    {
        PlayerInteraction.onPlayerHoldingItem += ColliderStateChange;
    }

    private void OnDisable()
    {
        PlayerInteraction.onPlayerHoldingItem -= ColliderStateChange;
    }

    public void Initialize()
    {
        itemName = "Name";
        itemDescription = "Description";
        itemThrownText = "Item Thrown Away";
        throwBoxSO.itemsInBoxList.Clear();
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!thrownItems.Contains(other.GetComponent<ObjectGrabbable>().objectDetail.itemName))
        {
            thrownItems.Add(other.GetComponent<ObjectGrabbable>().objectDetail.itemName);

            if (other.GetComponent<ObjectGrabbable>().isHeld)
            {
                if (PlayerInteraction.onPlayerHoldingItem != null)
                {
                    other.GetComponent<ObjectGrabbable>().textToChange = textToChange;
                    throwBoxSO.itemsInBoxList.Add(other.GetComponent<ObjectGrabbable>().objectDetail.itemName);
                    ThrowAwayItem(other.GetComponent<ObjectGrabbable>().objectDetail);
                }
            }
        }
        else
        {
            ClearItem(other.GetComponent<ObjectGrabbable>().objectDetail);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (thrownItems.Contains(other.GetComponent<ObjectGrabbable>().objectDetail.itemName))
        {
            thrownItems.Remove(other.GetComponent<ObjectGrabbable>().objectDetail.itemName);
            if (PlayerInteraction.onPlayerHoldingItem != null)
            {
                throwBoxSO.itemsInBoxList.Remove(other.GetComponent<ObjectGrabbable>().objectDetail.itemName);
                ClearItem(other.GetComponent<ObjectGrabbable>().objectDetail);
            }
        }
    }

    public void ThrowAwayItem(Object_Details objectInformation)
    {
        itemName = objectInformation.name;
        //itemDescription = objectGrabbed.description;
        textToChange.text = objectInformation.throwItem;   
    }

    public void ClearItem(Object_Details objectInformation)
    {
        itemName = null;
        //itemDescription = objectGrabbed.description;
        itemThrownText = null;
        textToChange.text = "";
    }

    private void ColliderStateChange(bool enabled)
    {
        colliderState.enabled = enabled;
        textToChange.text = "";

    }
}
