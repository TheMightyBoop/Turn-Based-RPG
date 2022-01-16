using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
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
