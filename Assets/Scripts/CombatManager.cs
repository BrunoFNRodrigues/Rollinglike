using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public enum CombatState {START, LOCK_ACTION, PLAYERTURN, ENEMYTURN, WON, LOST}


public class CombatManager : MonoBehaviour
{   
    public Text systemDialogue;

    public GameObject EnemyObject;
    public GameObject playerObject;


    UnitInfo enemyUnit;
    UnitInfo playerUnit;
   

    public CombatState state;

    public EnemyHUDController enemyHUD;
    public PlayerHUD playerHUD;

    // Start is called before the first frame update
    void Start()
    {
        state = CombatState.START;
        StartCoroutine(SetupCombat());
    }   

    IEnumerator SetupCombat(){
       GameObject enemyGO = Instantiate(EnemyObject);  
       enemyUnit = enemyGO.GetComponent<UnitInfo>();

       GameObject playerGO = Instantiate(playerObject);  
       playerUnit = playerGO.GetComponent<UnitInfo>();

       systemDialogue.text = "The " + enemyUnit.unitName + " readies his weapon.";

       enemyHUD.setHUD(enemyUnit); 
       playerHUD.setHUD(playerUnit); 

        //Espera 3 segundos para mudar de estado
       yield return new WaitForSeconds(3f);

       state = CombatState.PLAYERTURN;
       PlayerTurn();
    }

    void PlayerTurn(){
        systemDialogue.text = "What will the player do?";
    }

    public void OnAttackButton(){
        if(state != CombatState.PLAYERTURN)
            return;
    
        StartCoroutine(PlayerAttack());
    }

    IEnumerator PlayerAttack(){
        state = CombatState.LOCK_ACTION;
        bool isDead = enemyUnit.TakeDamage(playerUnit.str);

        enemyHUD.updateHP(enemyUnit.currHP);
        systemDialogue.text = "The player attacks!";

        yield return new WaitForSeconds(2f);

        if(isDead){
            state = CombatState.WON;
            StartCoroutine(EndCombat());
        }else{
            state = CombatState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn(){
        systemDialogue.text = enemyUnit.unitName + " lands a blow";

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.str);

        playerHUD.updateHP(playerUnit);

        yield return new WaitForSeconds(1f);

        if(isDead){
            state = CombatState.LOST;
            StartCoroutine(EndCombat());
        }else{
            state = CombatState.PLAYERTURN;
            PlayerTurn();
        }

    }

    IEnumerator EndCombat(){
        if (state == CombatState.WON){
            systemDialogue.text = "The player emerges victorious";
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("RoomStart");
        }else if (state == CombatState.LOST){
            systemDialogue.text = "The player is defeated..";
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("RoomStart");
        }
    }
}
