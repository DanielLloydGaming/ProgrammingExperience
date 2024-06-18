using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeepBoxManager : MonoBehaviour, IInitializeable, IKeepable
{
    [SerializeField] public static List<string> keptItems = new List<string>();
    [SerializeField] public TextMeshProUGUI textToChange;
    [SerializeField] private string itemName;
    [SerializeField] private string itemDescription;
    [SerializeField] private string itemKeepText;
    [SerializeField] private BoxCollider colliderState;
    [SerializeField] public  BoxListSO keepBoxSO;

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
        itemKeepText = "Item Kept";
        textToChange.text = "";
        keepBoxSO.itemsInBoxList.Clear();
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!keptItems.Contains(other.GetComponent<ObjectGrabbable>().objectDetail.itemName))
        {
            keptItems.Add(other.GetComponent<ObjectGrabbable>().objectDetail.itemName);

            if (other.GetComponent<ObjectGrabbable>().isHeld)
            {
                if (PlayerInteraction.onPlayerHoldingItem != null)
                {
                    other.GetComponent<ObjectGrabbable>().textToChange = textToChange;
                    keepBoxSO.itemsInBoxList.Add(other.GetComponent<ObjectGrabbable>().objectDetail.itemName);
                    KeepItem(other.GetComponent<ObjectGrabbable>().objectDetail);
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
        if (keptItems.Contains(other.GetComponent<ObjectGrabbable>().objectDetail.itemName))
        {
            keptItems.Remove(other.GetComponent<ObjectGrabbable>().objectDetail.itemName);

            if (PlayerInteraction.onPlayerHoldingItem != null)
            {
                keepBoxSO.itemsInBoxList.Remove(other.GetComponent<ObjectGrabbable>().objectDetail.itemName);
                ClearItem(other.GetComponent<ObjectGrabbable>().objectDetail);
            }
        }
    }

    public void KeepItem(Object_Details objectInformation)
    {
        itemName = objectInformation.name;
        //itemDescription = objectGrabbed.description;
        textToChange.text = objectInformation.keepItem;       
    }

    public void ClearItem(Object_Details objectInformation)
    {
        itemName = null;
        //itemDescription = objectGrabbed.description;
        itemKeepText = null;
        textToChange.text = "";
    }

    private void ColliderStateChange(bool enabled)
    {
        colliderState.enabled = enabled;
        textToChange.text = "";
        if(!enabled)
        {
        }
    }
}
