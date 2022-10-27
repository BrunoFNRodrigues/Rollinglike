using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public float maxHealth;
    public float Health;
    // Start is called before the first frame update
    void Start()
    {
        slider.maxValue = PlayerPrefs.GetFloat("maxHealth");
        slider.value = PlayerPrefs.GetFloat("Health");
    }

    private void Update()
    {
        PlayerPrefs.SetFloat("maxHealth", maxHealth);
        PlayerPrefs.SetFloat("Health", Health);
    }
}
