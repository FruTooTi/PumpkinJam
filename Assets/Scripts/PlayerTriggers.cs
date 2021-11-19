using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggers : MonoBehaviour
{
    public IInteractable interactionObject;
    public Hook hook;

    public static PlayerTriggers Instance;
    void Start()
    {
        Instance = this;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            hook.StretchHook();
        }
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
