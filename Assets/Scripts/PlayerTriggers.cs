using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggers : MonoBehaviour
{
    public IInteractable interactionObject;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Interactable"))
        {
            IInteractable interactable = other.GetComponent<IInteractable>();
            if (interactable == null){ return; }

            interactionObject = interactable;
            if (interactionObject.autoInteract)
            {
                interactionObject.Interact();
            }
            else
            {
                GameManager.Instance.ShowInteractionMessage(interactionObject);
            }
        } 
        else if (other.CompareTag("LevelEnd"))
        {
            GameManager.Instance.LevelUp();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Interactable")) { return; }
        IInteractable interactable = other.GetComponent<IInteractable>();
        if (interactable == null){ return; }

        interactionObject = null;
        GameManager.Instance.SetActiveInteractionMessage(false);
    }
}
