using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    public InteractableData interactableData;
    public GameObject chatBubbleIndicator;
    public GameEvent OnInteract;
    private void Start()
    {
        IndicatorOff();
    }

    public void IndicatorOn()
    {
        chatBubbleIndicator.SetActive(true);
    }

    public void IndicatorOff()
    {
        chatBubbleIndicator.SetActive(false);
    }

    public void Interact()
    {
        OnInteract.Raise();
    }
}
