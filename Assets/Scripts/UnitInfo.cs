using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfo : MonoBehaviour
{
    public string unitName;
    public int str;
    public int HP;
    public int currHP;

    public bool TakeDamage(int dmg){
        currHP -= dmg;

        if (currHP <= 0)
            return true;
        else 
            return false; 
    }
}
