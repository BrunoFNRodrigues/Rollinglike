using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public int maxHealth;
    public int Health;
    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = GlobalPlayer.maxHealth;
        slider.value = GlobalPlayer.currHealth;
    }


}
