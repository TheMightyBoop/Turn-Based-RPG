using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthStatusData", menuName = "StatusObjects/Health", order = 53)]
public class CharacterStatus : ScriptableObject
{
    public string charName = "name";
    public float[] position = new float[2];
    public GameObject characterGameObject;
    public float maxHealth = 100;
    public float health = 100;

}
