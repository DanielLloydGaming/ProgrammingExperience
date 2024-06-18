using UnityEngine;

public class HighlightObjects : MonoBehaviour, IInitializeable, IRaycastSight
{
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private Transform highlightedObject;
    [SerializeField] private LayerMask pickUpLayerMask;
    [SerializeField] private float pickUpDistance = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Raycast();   
    }

    /// <summary>
    /// Sets up any variables and references that this script needs to function.
    /// </summary>
    public void Initialize()
    {
        playerCameraTransform = GetComponent<Camera>().transform;
    }

    /// <summary>
    /// Checks if we are currently highlighting a selected object, if not then if it is in the Object Layer, then it is highlighted.
    /// </summary>
    public void Raycast()
    {
        if (highlightedObject != null)
        {
            highlightedObject.gameObject.GetComponent<Outline>().enabled = false;
            highlightedObject = null;
        }
        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickUpDistance, pickUpLayerMask))
        {
            highlightedObject = raycastHit.transform;
            if (highlightedObject != null)
            {
                if (highlightedObject.gameObject.GetComponent<Outline>() != null)
                {
                    highlightedObject.gameObject.GetComponent<Outline>().enabled = true;
                }
                else
                {
                    Outline outline = highlightedObject.gameObject.AddComponent<Outline>();
                    outline.enabled = true;
                    highlightedObject.gameObject.GetComponent<Outline>().OutlineColor = Color.green;
                    highlightedObject.gameObject.GetComponent<Outline>().OutlineWidth = 7.0f;
                }
            }
            else
            {
                highlightedObject = null;
            }
        }
    }
}
