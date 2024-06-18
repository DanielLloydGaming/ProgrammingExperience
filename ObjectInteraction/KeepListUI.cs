using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeepListUI : MonoBehaviour
{
    [SerializeField] private BoxListSO boxListSO;
    [SerializeField] private TextMeshProUGUI boxListText;
    [SerializeField] private InputActionReference debugAction;

    private void OnEnable()
    {
        debugAction.action.performed += DebugCall;
    }

    private void OnDisable()
    {
        debugAction.action.performed -= DebugCall;
    }

    private void Start()
    {
        boxListText.text = "";
    }

    void DebugCall(InputAction.CallbackContext debugAction)
    {
        WriteText();
    }

    private void WriteText()
    {
        for (int item = 0; item < boxListSO.itemsInBoxList.Count; item++)
        {
            boxListText.text += item + "\n";
        }
    }
}
