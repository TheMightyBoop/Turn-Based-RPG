using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, PLAYERACTION, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerSpawnPoint;
    public Transform enemySpawnPoint;

    Unit playerUnit;
    Unit enemyUnit;

    public Text dialogueText;

    public BattleHUD playerHUD;

    public BattleState state;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab, playerSpawnPoint);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO =  Instantiate(enemyPrefab, enemySpawnPoint);
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialogueText.text = $"The {enemyUnit.unitName} cometh...";

        playerHUD.SetHUD(playerUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack()
    {
        bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

        dialogueText.text = $"You did {playerUnit.damage} damage";

        yield return new WaitForSeconds(2f);

        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator PlayerHeal()
    {
        playerUnit.Heal();

        playerHUD.SetHUD(playerUnit);
        dialogueText.text = $"You healed {playerUnit.healAmount} HP";

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = $"{enemyUnit.unitName} attacks!";

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

        dialogueText.text = $"{enemyUnit.unitName} did {enemyUnit.damage} damage";

        playerHUD.SetHUD(playerUnit);

        yield return new WaitForSeconds(1f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        } else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }

    void EndBattle()
    {
        if(state == BattleState.WON)
        {
            dialogueText.text = "You Defeated";
        } else if (state == BattleState.LOST)
        {
            dialogueText.text = "You were defeated";
        }
    }

    void PlayerTurn()
    {
        dialogueText.text = "Your turn...";
    }

    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        state = BattleState.PLAYERACTION;
        StartCoroutine(PlayerAttack());
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        state = BattleState.PLAYERACTION;
        StartCoroutine(PlayerHeal());
    }

}
