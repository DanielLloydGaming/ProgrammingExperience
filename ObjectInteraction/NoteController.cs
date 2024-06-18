using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using StarterAssets;

public class NoteController : MonoBehaviour, IInitializeable, IInteractable
{
    [Header("UI References")]
    [SerializeField] private GameObject noteUI;
    [SerializeField] private Sprite noteImage;
    [SerializeField] private bool isReading = false;

    [Space(10)]
    [Header("Player Reference")]
    [SerializeField] private FirstPersonController player;
    [SerializeField] private PlayerInput playerActionMap;
    [SerializeField] private InputActionReference closeNoteKey;

    private void OnEnable()
    {
        closeNoteKey.action.performed += CloseNote;
    }

    private void OnDisable()
    {
        closeNoteKey.action.performed -= CloseNote;
    }

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        player = FindAnyObjectByType<FirstPersonController>();
        playerActionMap = FindAnyObjectByType<PlayerInput>();
    }

    public void CloseNote(InputAction.CallbackContext closeNote)
    {
        StopInteraction();
    }

    /// <summary>
    /// Reading Interaction.
    /// </summary>
    public void StartInteraction()
    {
        noteUI.SetActive(true);
        playerActionMap.SwitchCurrentActionMap("Interaction");
        PlayerReading(true);
        isReading = true;
    }

    /// <summary>
    /// Closing Interaction
    /// </summary>
    public void StopInteraction()
    {
        noteUI.SetActive(false);
        playerActionMap.SwitchCurrentActionMap("Player");
        PlayerReading(false);
        isReading = false;
    }

    private void PlayerReading(bool isReading)
    {
        player.enabled = !isReading;
    }
}
