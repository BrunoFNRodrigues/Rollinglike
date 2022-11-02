using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthHandler : MonoBehaviour
{   
    public int hp;
    
    public Slider slider;
    void Start(){
        slider.value = hp;
    }
    // Update is called once per frame
    public void DamageHealth(int damage)
    {
        hp -= damage;
        slider.value = hp;
    }
}
