using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum CombatState {START, LOCK_ACTION, INITPHASE,PLAYERTURN, ENEMYTURN, ENDPHASE, WON, LOST}


public class CombatManager : MonoBehaviour
{     
    // Dicionário de combinações
    public Dictionary<string, string> Combinations = new Dictionary<string, string>()
    {   
        {"Water+Water", "Water v2"},
        {"Water+Fire", "..."},
        {"Water+Earth", "Mud"},
        {"Water+Electric", "Steam"}, //Mudar
        {"Fire+Earth", "Magma"},
        {"Fire+Electric", "Plasma"},
        {"Fire+Fire", "Fire v2"},
        {"Earth+Electric", "..."},
        {"Earth+Earth", "Earth v2"},
        {"Electric+Electric", "Electric v2"},

        {"Water+Life", "Healing Touch"},
        {"Fire+Life", "Phoenix Rise"},
        {"Earth+Life", "Earth`s Blessing"},
        {"Electric+Life", "Charge"},

        {"Water+Death", "Poison"},
        {"Fire+Death", "Hellfire"},
        {"Earth+Death", "Earthquake"},
        {"Electric+Death", "Electrocution"},
    };


    public GameObject[] all_enemies;

    public int randomEnemy;

    // Essas arrays deverão pegar as spells que o player estiver equipado (por enquanto estão fixas)
    public List<string> ElementalSpells = new List<string>();
    public List<string> SpecialSpells = new List<string>();


    public Text systemDialogue;
    public Text spellPreview;

    //public GameObject EnemyObject = all_enemies[randomEnemy];
    public GameObject playerObject;


    UnitInfo enemyUnit;
    UnitInfo playerUnit;

    public Command_btn CommandBtn1;
    public Command_btn CommandBtn2;
    public Command_btn CommandBtn3;
    public Command_btn CommandBtn4;

    public CombatState state;

    public EnemyHUDController enemyHUD;
    public PlayerHUD playerHUD;

    public int selectedSpells = 0;

    public string castedSpell;

    // Start is called before the first frame update
    void Start()
    {
        //Inicializa o espacos de magia
        if (GlobalMagicSlots.fireLevel > 0)
        {
            ElementalSpells.Add("Fire");
        }

        if (GlobalMagicSlots.waterLevel > 0)
        {
            ElementalSpells.Add("Water");
        }

        if (GlobalMagicSlots.earthLevel > 0)
        {
            ElementalSpells.Add("Earth");
        }

        if (GlobalMagicSlots.eletricLevel > 0)
        {
            ElementalSpells.Add("Electric");
        }

        if (GlobalMagicSlots.lifeLevel > 0)
        {
            SpecialSpells.Add("Life");
        }

        if (GlobalMagicSlots.deathLevel > 0)
        {
            SpecialSpells.Add("Death");
        }


        randomEnemy = Random.Range(0, 6);
        state = CombatState.START;
        StartCoroutine(SetupCombat());
    }   

    IEnumerator SetupCombat(){
       GameObject enemyGO = Instantiate(all_enemies[randomEnemy]);  
       enemyUnit = enemyGO.GetComponent<UnitInfo>();

       GameObject playerGO = Instantiate(playerObject);  
       playerUnit = playerGO.GetComponent<UnitInfo>();

        GlobalPlayer.maxHealth = playerUnit.currHP;

       CommandBtn1.setSpells();
       CommandBtn2.setSpells();
       CommandBtn3.setSpells();
       CommandBtn4.setSpells();

       systemDialogue.text = "The " + enemyUnit.unitName + " readies his weapon.";
       spellPreview.text = " ";

       enemyHUD.setHUD(enemyUnit); 
       playerHUD.setHUD(playerUnit);

            //Espera 3 segundos para mudar de estado
        yield return new WaitForSeconds(3f);

        if (ElementalSpells.Count == 0 || SpecialSpells.Count == 0)
        {
            systemDialogue.text = "The player did not equip any spell. The enemy instakills the player!";
            yield return new WaitForSeconds(3f);
            state = CombatState.LOST;
            StartCoroutine(EndCombat());
        }
        else{
            state = CombatState.PLAYERTURN;
            PlayerTurn();
        }  
    }

    void PlayerTurn(){
        // Escolha do jogador
        string spell1 = ElementalSpells[UnityEngine.Random.Range (0, ElementalSpells.Count)];
        string spell2 = ElementalSpells[UnityEngine.Random.Range (0, ElementalSpells.Count)];
        string spell3 = ElementalSpells[UnityEngine.Random.Range (0, ElementalSpells.Count)];
        string spell4 = SpecialSpells[UnityEngine.Random.Range (0, SpecialSpells.Count)];
        CommandBtn1.updateSpell(spell1);
        CommandBtn2.updateSpell(spell2);
        CommandBtn3.updateSpell(spell3);
        CommandBtn4.updateSpell(spell4);
        systemDialogue.text = "What will the player do?";
    }

    public void CastSpell(){
        if(state != CombatState.PLAYERTURN || selectedSpells != 2)
            return;
    
        if (playerUnit.currSpd >= enemyUnit.currSpd){
            spellPreview.text = " ";
            selectedSpells = 0;
            StartCoroutine(PlayerAttack(true));
            
        }else{
            state = CombatState.ENEMYTURN;
            spellPreview.text = " ";
            selectedSpells = 0;
            StartCoroutine(EnemyTurn(true));
        }
    }
    
    public void PreviewSpell(){
        if (selectedSpells == 2){
            List<string> spellList = new List<string>();
            if (CommandBtn1.pressed){
                spellList.Add(CommandBtn1.spellName.text);
            }
            if (CommandBtn2.pressed){
                spellList.Add(CommandBtn2.spellName.text);
            }
            if (CommandBtn3.pressed){
                spellList.Add(CommandBtn3.spellName.text);
            }
            if (CommandBtn4.pressed){
                spellList.Add(CommandBtn4.spellName.text);
            }
            try{
                spellPreview.text = spellList[0] + " + " + spellList[1] + " = " + Combinations[spellList[0] + "+" + spellList[1]];
                castedSpell = Combinations[spellList[0] + "+" + spellList[1]];
            }catch{
                spellPreview.text = spellList[1] + " + " + spellList[0] + " = " +  Combinations[spellList[1] + "+" + spellList[0]];
                castedSpell = Combinations[spellList[1] + "+" + spellList[0]];
            }

        } else {
            spellPreview.text = " ";
        }
    }

    public void OnClickCommand1(){
        if (selectedSpells < 2 || CommandBtn1.pressed){
            if (CommandBtn1.invertStatus()){
                selectedSpells += 1;
            }else{
                selectedSpells -= 1;
            }
        }
        PreviewSpell();
    }

    public void OnClickCommand2(){
        if (selectedSpells < 2 || CommandBtn2.pressed){
            if (CommandBtn2.invertStatus()){
                selectedSpells += 1;
            }else{
                selectedSpells -= 1;
            }
        }
        PreviewSpell();
    }

    public void OnClickCommand3(){
        if (selectedSpells < 2 || CommandBtn3.pressed){
            if (CommandBtn3.invertStatus()){
                selectedSpells += 1;
            }else{
                selectedSpells -= 1;
            }
        }
        PreviewSpell();
    }

    public void OnClickCommand4(){
        if (selectedSpells < 2 || CommandBtn4.pressed){
            if (CommandBtn4.invertStatus()){
                selectedSpells += 1;
            }else{
                selectedSpells -= 1;
            }
        }
        PreviewSpell();
    }

    IEnumerator PlayerAttack(bool attck_first){
        // Ação do player
        state = CombatState.LOCK_ACTION;

        float attackChance = Random.Range(0,100);
        if (attackChance <= playerUnit.currAcc && playerUnit.paralyzedStatus == 0){
            systemDialogue.text = "The player casts " + castedSpell + "!";

            yield return new WaitForSeconds(2f);

            Spell(castedSpell);

            yield return new WaitForSeconds(2f);

            playerHUD.updateHP(playerUnit);
            enemyHUD.updateHP(enemyUnit.currHP);
        }else{
            systemDialogue.text = "The player misses it's attack!";
        }

        yield return new WaitForSeconds(2f);

        if(enemyUnit.currHP <= 0){
            state = CombatState.WON;
            StartCoroutine(EndCombat());
        }else{
            if (attck_first){
                state = CombatState.ENEMYTURN;
                StartCoroutine(EnemyTurn(false));
            }else{
                state = CombatState.ENDPHASE;
                StartCoroutine(EndPhase());
            }
        }
    }

    IEnumerator EnemyTurn(bool attck_first){
        //Turno no inimigo
        float attackChance = Random.Range(0,100);
        if (attackChance <= enemyUnit.currAcc && enemyUnit.paralyzedStatus == 0){
            playerUnit.TakeDamage(1, enemyUnit.currStr);
            systemDialogue.text = enemyUnit.unitName + " lands a blow!";
            playerHUD.updateHP(playerUnit);
            enemyHUD.updateHP(enemyUnit.currHP);
        }else{
            if (enemyUnit.paralyzedStatus != 0){
                systemDialogue.text = enemyUnit.unitName + " is paralyzed!";
            }else{
                systemDialogue.text = enemyUnit.unitName + " misses it's attack!";
            }
            
        }

        yield return new WaitForSeconds(2f);

        if(playerUnit.currHP <= 0){
            state = CombatState.LOST;
            StartCoroutine(EndCombat());
        }else{
            if(attck_first){
                StartCoroutine(PlayerAttack(false));
            }else{
                state = CombatState.ENDPHASE;
                StartCoroutine(EndPhase());
            }
        }
    }

    IEnumerator EndPhase(){
        //END PHASE, aplica status
        if (playerUnit.poisonedStatus > 0){
            systemDialogue.text = "The player takes the poison damage!";
            playerUnit.poisonDamage();
            playerHUD.updateHP(playerUnit);
            yield return new WaitForSeconds(2f);

            if (playerUnit.currHP <= 0){
                state = CombatState.LOST;
                StartCoroutine(EndCombat());
            }
            playerUnit.poisonedStatus -= 1;
            if ( playerUnit.poisonedStatus == 0){
                systemDialogue.text = "The player is no longer poisoned!";
            }
        }

        if (enemyUnit.poisonedStatus > 0){
            systemDialogue.text = "The enemy takes the poison damage!";
            enemyUnit.poisonDamage();
            enemyHUD.updateHP(enemyUnit.currHP);
            yield return new WaitForSeconds(2f);

            if (enemyUnit.currHP <= 0){
                state = CombatState.WON;
                StartCoroutine(EndCombat());
            }
            enemyUnit.poisonedStatus -= 1;
            if ( enemyUnit.poisonedStatus == 0){
                systemDialogue.text = "The enemy is no longer poisoned!";
                yield return new WaitForSeconds(2f);
            }
        }
    /**************************************************************************/
        if(enemyUnit.paralyzedStatus > 0){
            enemyUnit.paralyzedStatus -= 1;
            if ( enemyUnit.paralyzedStatus == 0){
                systemDialogue.text = "The enemy is no longer paralyzed!";
                yield return new WaitForSeconds(2f);
            }
        }

        if(playerUnit.paralyzedStatus > 0){
            playerUnit.paralyzedStatus -= 1;
            if ( playerUnit.paralyzedStatus == 0){
                systemDialogue.text = "The player is no longer paralyzed!";
                yield return new WaitForSeconds(2f);
            }
        }
    /***************************************************************************/
        if(playerUnit.healingStatus > 0){
            systemDialogue.text = "The player's Healing Touch takes effect!";
            playerUnit.healUnit(0.3f);
            playerHUD.updateHP(playerUnit);
            yield return new WaitForSeconds(2f);

            playerUnit.healingStatus -= 1;
            if ( playerUnit.healingStatus == 0){
                systemDialogue.text = "The player's Healing Touch wears off!";
                yield return new WaitForSeconds(2f);
            }
        }

        if(enemyUnit.healingStatus > 0){
            systemDialogue.text = "The enemy's Healing Touch takes effect!";
            enemyUnit.healUnit(0.3f);
            enemyHUD.updateHP(enemyUnit.currHP);
            yield return new WaitForSeconds(2f);

            enemyUnit.healingStatus -= 1;
            if (enemyUnit.healingStatus == 0){
                systemDialogue.text = "The enemy's Healing Touch wears off!";
                yield return new WaitForSeconds(2f);
            }
        }
    /****************************************************************************/
        if(enemyUnit.chargedStatus > 0){
            enemyUnit.chargedStatus -= 1;
            if (enemyUnit.chargedStatus == 0){
                enemyUnit.currStr -= (int) Mathf.Ceil(2f * enemyUnit.str);
                systemDialogue.text = "The enemy is no longer charged!";
                yield return new WaitForSeconds(2f);
            }
        }

        if(playerUnit.chargedStatus > 0){
            playerUnit.chargedStatus -= 1;
            if (playerUnit.chargedStatus == 0){
                playerUnit.currStr -= (int) Mathf.Ceil(2f * playerUnit.str);
                systemDialogue.text = "The player is no longer charged!";
                yield return new WaitForSeconds(2f);
            }
        }


        state = CombatState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator EndCombat(){
        //FIM DO COMBATE
        if (state == CombatState.WON){
            systemDialogue.text = "The player emerges victorious";
            GlobalPlayer.currHealth = playerUnit.currHP;
            GlobalPlayer.winCounter++;
            yield return new WaitForSeconds(1f);
            if (GlobalPlayer.winCounter == 3)
            {
                SceneManager.LoadScene("VictoryScreen");
            } else
            {

                SceneManager.LoadScene(Random.Range(1,8));
            }
        }
        else if (state == CombatState.LOST){
            systemDialogue.text = "The player is defeated..";
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("MainMenu");
        }
    }


    //perdão para futuros que verem este espaguete   
    public void Spell(string spellName){
        switch(spellName){
            case"Water v2":
            //Cura bastante com base na vida perdida
                playerUnit.healUnit(0.2f * GlobalMagicSlots.waterLevel);
                systemDialogue.text = "The player is greatly healed!";
                break;
            case"Mud":
            // Diminui speed do inimigo, cura por valor pequeno e dá dano pequeno
                playerUnit.healUnit(0.1f);
                enemyUnit.currSpd -= (int) Mathf.Ceil((0.1f * GlobalMagicSlots.waterLevel) * enemyUnit.spd);
                systemDialogue.text = "Enemy speed and accuracy are lowered. Player is healed.";
                enemyUnit.TakeDamage(0.2f * GlobalMagicSlots.earthLevel, playerUnit.currStr);
                break;
            case"Steam":
            // Diminui a acurácia do inimigo e dá dano pequeno
                enemyUnit.currAcc -= (int) Mathf.Ceil((0.05f * GlobalMagicSlots.waterLevel) * enemyUnit.acc);
                systemDialogue.text = "Enemy accuracy is lowered.";
                enemyUnit.TakeDamage(0.2f * GlobalMagicSlots.eletricLevel, playerUnit.currStr);
                break;
            case"Magma":
            // Aumenta razoavelmente o dano e defesa. Causa dano normal
                playerUnit.currStr += (int) Mathf.Ceil((0.1f * GlobalMagicSlots.fireLevel) * playerUnit.str);
                playerUnit.currDef += (int) Mathf.Ceil((0.1f * GlobalMagicSlots.earthLevel) * playerUnit.def);
                systemDialogue.text = "Player defense and strength are buffed.";
                enemyUnit.TakeDamage(1f, playerUnit.currStr);
                break;
            case"Plasma":
            // Aumenta razoavelmente o dano e velocidade. Causa dano normal
                playerUnit.currStr += (int) Mathf.Ceil((0.1f * GlobalMagicSlots.fireLevel) * playerUnit.str);
                playerUnit.currSpd += (int) Mathf.Ceil((0.1f * GlobalMagicSlots.eletricLevel) * playerUnit.spd);
                systemDialogue.text = "Player speed and strength are buffed.";
                enemyUnit.TakeDamage(1f, playerUnit.currStr);
                break;
            case"Fire v2":
            // Aumenta bastante o dano. Causa dano normal
                playerUnit.currStr += (int) Mathf.Ceil((0.1f * GlobalMagicSlots.fireLevel) * playerUnit.str);
                systemDialogue.text = "Player strength is greatly buffed!";
                enemyUnit.TakeDamage(1f, playerUnit.currStr);
                break;
            case"Earth v2":
            // Aumenta bastante a defesa. Causa dano baixo
                playerUnit.currDef += (int) Mathf.Ceil((0.1f * GlobalMagicSlots.earthLevel) * playerUnit.def);
                systemDialogue.text = "Player defense is greatly buffed!";
                enemyUnit.TakeDamage(0.2f, playerUnit.currStr);
                break;
            case"Electric v2":
            // Aumenta bastante a velocidade. Causa dano médio
                playerUnit.currSpd += (int) Mathf.Ceil((0.2f * GlobalMagicSlots.eletricLevel) * playerUnit.spd);
                systemDialogue.text = "Player speed is greatly buffed!";
                enemyUnit.TakeDamage(0.5f, playerUnit.currStr);
                break;

            case"Healing Touch":
            // Põe benção de cura por 3 turnos. Recupera HP gradualmente
                playerUnit.healingStatus = 2 + GlobalMagicSlots.waterLevel;
                systemDialogue.text = "The player is gradually healing!";
                break;
            case"Phoenix Rise":
            // Aumenta todos os status razoavelmente
                playerUnit.currStr += (int) Mathf.Ceil(0.2f * playerUnit.str);
                playerUnit.currSpd += (int) Mathf.Ceil(0.2f * playerUnit.spd);
                playerUnit.currDef += (int) Mathf.Ceil(0.2f * playerUnit.def);
                enemyUnit.currAcc  += (int) Mathf.Ceil(0.2f * enemyUnit.acc);
                systemDialogue.text = "All status are buffed!";
                break;
            case"Earth`s Blessing":
            // Retira todos debuffs (por enquanto só tem envenenamento..)
                playerUnit.poisonedStatus = 0;
                systemDialogue.text = "The player is no longer in a bad condition!";
                break;
            case"Charge":
            //  Aumenta bastante o dano no próximo turno mas reduz bastante a defesa
                playerUnit.chargedStatus = 2;
                playerUnit.currDef -= (int) Mathf.Ceil(0.8f * playerUnit.def);
                playerUnit.currStr += (int) Mathf.Ceil((GlobalMagicSlots.eletricLevel + 1) * playerUnit.str);
                systemDialogue.text = "The player is charging for the next turn!";
                break;

            case"Poison":
            // Envenena o inimigo por 3 turnos. Recebe dano gradualmente
                enemyUnit.poisonedStatus = 2 + GlobalMagicSlots.waterLevel;
                systemDialogue.text = "The enemy is poisoned!";
                break;
            case"Hellfire":
            // Causa dano massivo mas sacrifica 1/2 da vida atual e perde força
                enemyUnit.TakeDamage(2f, playerUnit.currStr);
                playerUnit.currHP -= (int) Mathf.Floor(0.5f * playerUnit.currHP);
                playerUnit.currStr -= (int) Mathf.Ceil(0.3f * playerUnit.str);
                systemDialogue.text = "Hellfire damages the player and reduces it's strength!";
                break;
            case"Earthquake":
            // Causa dano massivo mas diminui bsatante todos os status 
                enemyUnit.TakeDamage(2f, playerUnit.currStr);
                playerUnit.currStr -= (int) Mathf.Ceil(0.5f * playerUnit.str);
                playerUnit.currSpd -= (int) Mathf.Ceil(0.5f * playerUnit.spd);
                playerUnit.currDef -= (int) Mathf.Ceil(0.5f * playerUnit.def);
                enemyUnit.currAcc  -= (int) Mathf.Ceil(0.5f * enemyUnit.acc);
                systemDialogue.text = "Earthquake tires the player and reduces all status!";
                break;
            case"Electrocution":
            // Causa dano pequeno e deixa o alvo paralizado por 1 turno. Não pode atacar
                enemyUnit.paralyzedStatus = 2;
                enemyUnit.TakeDamage(0.1f, playerUnit.currStr);
                systemDialogue.text = "The enemy is paralyzed!";
                break;
            case"...":
            // Combinação ruim.
                systemDialogue.text = "The player casted a bad combination! Nothing happened...";
                break;
            default:
                break;
        }
    }
}
