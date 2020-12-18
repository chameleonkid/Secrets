using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManaManager : MonoBehaviour
{
    [SerializeField] private FloatMeter mana = default;
    [SerializeField] private TextMeshProUGUI manaText = default;
    // ^ Shouldn't reassign during runtime unless
    // care is taken to unsubscribe from events.
    [SerializeField] private Image manaSlider = default;

    private void OnEnable()
    {
        mana.OnMinChanged += UpdateUI;
        mana.OnMaxChanged += UpdateUI;
        mana.OnCurrentChanged += UpdateUI;
    }
    private void OnDisable()
    {
        mana.OnMinChanged -= UpdateUI;
        mana.OnMaxChanged -= UpdateUI;
        mana.OnCurrentChanged -= UpdateUI;
    }

    private void Start() => UpdateUI();

    private void UpdateUI()
    {
    //    manaSlider.minValue = mana.min;
    //    manaSlider.maxValue = mana.max;
        manaSlider.fillAmount = mana.current / mana.max;
        manaText.text = "" + mana.current + " / " + mana.max;
    }
}
