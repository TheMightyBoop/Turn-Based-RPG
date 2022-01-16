using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{

    public Text hpText;

    public void SetHUD(Unit unit)
    {
        hpText.text = $"HP: {unit.currentHP} / {unit.maxHP}"; 
    }

}
