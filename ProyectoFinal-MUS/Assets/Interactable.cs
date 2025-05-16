using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public abstract void Interact();

    private GameObject _otherInteractable = null;
    private void OnTriggerEnter(Collider other)
    {
        PlayerInteraction player = other.GetComponent<PlayerInteraction>();
        if (player != null)
        {
            Interactable otherInt = player.GetCurrentInteractable();
            if (otherInt == null) _otherInteractable = null;
            else _otherInteractable = otherInt.gameObject;

            player.SetCurrentInteractable(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerInteraction player = other.GetComponent<PlayerInteraction>();
        if (player != null)
        {
            if(_otherInteractable != null) 
                player.SetCurrentInteractable(_otherInteractable.GetComponent<Interactable>());
            
            _otherInteractable = null;
        }
    }
}
