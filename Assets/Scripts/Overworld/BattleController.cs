using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public CharacterStatus playerStatus;
    public CharacterStatus enemyStatus;

    private bool isAttacked;

    private void Start()
    {
        isAttacked = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        enemyStatus = other.gameObject.GetComponent<EnemyStatus>().enemyStatus;

        if (this.playerStatus.health > 0)
        {
            if (other.CompareTag("Enemy"))
            {
                if (!isAttacked)
                {
                    isAttacked = true;
                    SetBattleData(enemyStatus);
                    LevelLoader.instance.LoadLevel("BattleScene_Test");
                }
            }
        }
    }

    public void SetBattleData(CharacterStatus status)
    {
        // Player Data 
        playerStatus.position[0] = this.transform.position.x;
        playerStatus.position[1] = this.transform.position.y;

        //Enemy Data
        enemyStatus.charName = status.charName;
        enemyStatus.characterGameObject = status.characterGameObject.transform.GetChild(0).gameObject;
        enemyStatus.health = status.health;
        enemyStatus.maxHealth = status.maxHealth;
    }
}
