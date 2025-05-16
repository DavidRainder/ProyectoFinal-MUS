using TMPro;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _interactionControls = null;

    Interactable _currentInteractable = null;

    private void Awake()
    {
        _interactionControls.enabled = false;
    }

    public void SetCurrentInteractable(Interactable interactable)
    {
        _currentInteractable = interactable;
        _interactionControls.enabled = _currentInteractable != null;
    }

    public Interactable GetCurrentInteractable() { return _currentInteractable; }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            _currentInteractable?.Interact();
        }
    }
}
