using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InteractController : MonoBehaviour
{
    private IInteractable interactingObject;

    private bool isTriggeringInteractable;
    private bool dialogueTriggered;

    [SerializeField] private Text interactText;

    private void Start()
    {
        interactText.text = string.Empty;
    }

    private void Update()
    {
        if (interactingObject != null)
        {
            if (isTriggeringInteractable && !dialogueTriggered)
            {
                interactingObject.IndicatorOn();

                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactingObject.Interact();
                    dialogueTriggered = true;
                    interactingObject.IndicatorOff();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        interactingObject = other.GetComponent<IInteractable>();
        isTriggeringInteractable = true;
    }

    private void OnTriggerExit(Collider other)
    {
        isTriggeringInteractable = false;
        dialogueTriggered = false;
        interactingObject.IndicatorOff();
        interactingObject = null;
        interactText.text = string.Empty;
    }

    public void UpdateInteractText(InteractableData interactableData)
    {
        interactText.text = interactableData.InteractableDescription;
    }
}
