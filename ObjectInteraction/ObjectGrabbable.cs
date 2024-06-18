using TMPro;
using UnityEngine;

public class ObjectGrabbable : MonoBehaviour, IInitializeable, IPhysicCalculation
{
    [SerializeField] private Rigidbody objectRigidbody;
    [SerializeField] private Transform objectGrabPointTransform;
    [SerializeField] public Object_Details objectDetail;
    [SerializeField] public bool isHeld;
    [SerializeField] public TextMeshProUGUI textToChange;

    private void Start()
    {
        Initialize();
    }
    public void Grab(Transform objectGrabPointTransform)
    {
        PlayerInteraction.onPlayerHoldingItem(true);
        this.objectGrabPointTransform = objectGrabPointTransform;
        this.isHeld = true;
        objectRigidbody.useGravity = false;
        if (textToChange != null)
        {
            textToChange.text = "";
        }

        // Using events we can connect the UI.
    }

    public void Drop()
    {
        PlayerInteraction.onPlayerHoldingItem(false);
        this.objectGrabPointTransform = null;
        this.isHeld = false;
        objectRigidbody.useGravity = true;
        if (textToChange != null)
        {
            textToChange.text = "";
        }
    }

    private void FixedUpdate()
    {
        CalculatePhysics();
    }

    public void Initialize()
    {
        objectRigidbody = GetComponent<Rigidbody>();
        
    }

    public void CalculatePhysics()
    {
        if (objectGrabPointTransform != null)
        {
            float lerpSpeed = 10f;
            Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);
            objectRigidbody.velocity = (objectGrabPointTransform.position - transform.position) / Time.fixedDeltaTime * 0.25f;
        }
    }
}
