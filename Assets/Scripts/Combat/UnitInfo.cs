using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfo : MonoBehaviour
{
    public string unitName;
    public int str;
    public int currStr;

    public int HP;
    public int currHP;
    
    public int def;
    public int currDef;

    public int spd;
    public int currSpd;

    public int acc;
    public int currAcc;

    public int healingStatus = 0;
    public int paralyzedStatus = 0;
    public int poisonedStatus = 0;
    public int chargedStatus = 0;

    public void TakeDamage(float dmgMultiplier, int dmg){
        int damage = (int) Mathf.Ceil(dmgMultiplier * dmg) - currDef; 

        if(damage <= 0){
            damage = 1;
        }
        
        currHP -= damage;

        if (currHP < 0){
            currHP = 0;
        }
    }

    public void healUnit(float percentage){
        int missingHP = HP - currHP;
        currHP += (int) Mathf.Ceil(percentage * missingHP);
        if (currHP > HP){
            currHP = HP;
        }
    }

    public void poisonDamage(){
        currHP -= (int) Mathf.Ceil(0.1f * currHP);
        if (currHP < 0){
            currHP = 0;
        }
    }
}
