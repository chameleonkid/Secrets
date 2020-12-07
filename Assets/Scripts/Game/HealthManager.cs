using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    [SerializeField] private FloatMeter health = default;
    // ^ Shouldn't reassign during runtime unless
    // care is taken to unsubscribe from events.
    [SerializeField] private Slider healthSlider = default;

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
        healthSlider.minValue = health.min;
        healthSlider.maxValue = health.max;
        healthSlider.value = health.current;
    }
}
