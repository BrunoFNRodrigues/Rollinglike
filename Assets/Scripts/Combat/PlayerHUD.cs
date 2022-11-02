using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class PlayerHUD : MonoBehaviour
{
    public Text textHP;

    public void setHUD(UnitInfo unit){
        textHP.text = "HP " + unit.currHP.ToString() + "/" + unit.HP.ToString();
    }

    public void updateHP(UnitInfo unit){
        //Por enquanto faz a mesma coisa
        textHP.text = "HP " + unit.currHP.ToString() + "/" + unit.HP.ToString();
    }
}
