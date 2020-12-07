using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ArrowManager : MonoBehaviour
{
    [SerializeField] private FloatMeter arrows = default;
    // ^ Shouldn't reassign during runtime unless
    // care is taken to unsubscribe from events.
    [SerializeField] private TextMeshProUGUI ArrowDisplay = default;


    private void OnEnable()
    {
        arrows.OnMinChanged += UpdateUI;
        arrows.OnMaxChanged += UpdateUI;
        arrows.OnCurrentChanged += UpdateUI;
    }
    private void OnDisable()
    {
        arrows.OnMinChanged -= UpdateUI;
        arrows.OnMaxChanged -= UpdateUI;
        arrows.OnCurrentChanged -= UpdateUI;
    }

    private void Start() => UpdateUI();

    private void UpdateUI()
    {

        ArrowDisplay.text = "" + arrows.current;
    }
}
