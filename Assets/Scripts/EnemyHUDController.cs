using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyHUDController : MonoBehaviour
{
    public Text nameText;
    public Slider HealthBar;

    public void setHUD(UnitInfo unit){
        nameText.text = unit.unitName;
        HealthBar.maxValue = unit.HP;
        HealthBar.value = unit.currHP;
    }

    public void updateHP(int newHP){
        HealthBar.value = newHP;
    }
}
