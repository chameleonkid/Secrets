using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ConstrainedFloatDisplay : MonoBehaviour
{
    [SerializeField] private ConstrainedFloat constrainedFloat = default;
    [SerializeField] private TextMeshProUGUI textDisplay = default;
    // ^ Shouldn't reassign during runtime unless
    // care is taken to unsubscribe from events.
    [SerializeField] private Image slider = default;

    private void OnEnable()
    {
        constrainedFloat.OnMinChanged += UpdateUI;
        constrainedFloat.OnMaxChanged += UpdateUI;
        constrainedFloat.OnCurrentChanged += UpdateUI;
    }
    private void OnDisable()
    {
        constrainedFloat.OnMinChanged -= UpdateUI;
        constrainedFloat.OnMaxChanged -= UpdateUI;
        constrainedFloat.OnCurrentChanged -= UpdateUI;
    }

    private void Start() => UpdateUI();

    private void UpdateUI()
    {
        // healthSlider.minValue = health.min;
        // healthSlider.maxValue = health.max;
        slider.fillAmount = constrainedFloat.current / constrainedFloat.max;
        textDisplay.text = "" + constrainedFloat.current + " / " + constrainedFloat.max;
    }
}
