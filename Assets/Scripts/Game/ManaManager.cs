using UnityEngine;
using UnityEngine.UI;

public class ManaManager : MonoBehaviour
{
    [SerializeField] private FloatMeter mana = default;
    // ^ Shouldn't reassign during runtime unless
    // care is taken to unsubscribe from events.
    [SerializeField] private Slider manaSlider = default;

    private void OnEnable() => mana.OnCurrentChanged += UpdateUI;
    private void OnDisable() => mana.OnCurrentChanged -= UpdateUI;

    private void Start() => UpdateUI();

    private void UpdateUI()
    {
        manaSlider.minValue = mana.min;
        manaSlider.maxValue = mana.max;
        manaSlider.value = mana.current;
    }
}
