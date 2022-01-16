using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New InteractableData", menuName = "Interactable Data", order = 51)]
public class InteractableData : ScriptableObject
{
    [SerializeField] private string interactableDescription;

    public string InteractableDescription
    {
        get
        {
            return interactableDescription;
        }
    }
}
