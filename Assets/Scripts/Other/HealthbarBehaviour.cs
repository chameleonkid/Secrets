using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarBehaviour : MonoBehaviour
{

    [SerializeField] private Slider slider;
    [SerializeField] private Gradient gradient;
    [SerializeField] private Image fill;

    private void Awake()
    {
        slider = GetComponentInChildren<Slider>();
    }
    public void UpdateHealthBar(float currentValue, float maxValue)
    {   
        if(slider)
        {
            slider.value = currentValue / maxValue;
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }

    }

}
