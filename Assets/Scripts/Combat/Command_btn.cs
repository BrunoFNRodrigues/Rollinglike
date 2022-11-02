using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Command_btn : MonoBehaviour
{   

    public TMP_Text spellName;

    public bool pressed = false;

    public void setSpells(){
        spellName.SetText(" ");
    }

    public bool invertStatus(){
        pressed = !pressed;
        if(pressed){
            this.GetComponent<Image>().color = Color.red;
        }else{
            this.GetComponent<Image>().color = Color.white;
        }
        return pressed;
    }

    public void updateSpell(string new_spell){
        this.GetComponent<Image>().color = Color.white;
        pressed = false;
        spellName.SetText(new_spell);
    }
}

