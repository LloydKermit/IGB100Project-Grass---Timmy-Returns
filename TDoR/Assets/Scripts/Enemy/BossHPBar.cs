using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossHPBar : MonoBehaviour
{
    public Slider slider;
    public Image fill;
    public TextMeshProUGUI hpText;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        hpText.text = health.ToString() + "/" + slider.maxValue.ToString();
    }
    public void SetHealth(int health)
    {
        slider.value = health;
        hpText.text = health.ToString() + "/" + slider.maxValue.ToString();
    }
}
