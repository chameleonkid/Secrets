using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private FloatMeter health = default;
    [SerializeField] private TextMeshProUGUI healthText = default;
    // ^ Shouldn't reassign during runtime unless
    // care is taken to unsubscribe from events.
    [SerializeField] private Image healthSlider = default;

    private void OnEnable()
    {
        health.OnMinChanged += UpdateUI;
        health.OnMaxChanged += UpdateUI;
        health.OnCurrentChanged += UpdateUI;
    }
    private void OnDisable()
    {
        health.OnMinChanged -= UpdateUI;
        health.OnMaxChanged -= UpdateUI;
        health.OnCurrentChanged -= UpdateUI;
    }

    private void Start() => UpdateUI();

    private void UpdateUI()
    {
   //     healthSlider.minValue = health.min;
    //    healthSlider.maxValue = health.max;
        healthSlider.fillAmount = health.current / health.max;
        healthText.text = "" + health.current + " / " + health.max;
    }
}
