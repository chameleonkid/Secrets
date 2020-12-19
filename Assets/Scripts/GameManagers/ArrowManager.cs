using UnityEngine;
using TMPro;

public class ArrowManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ArrowDisplay = default;
    [SerializeField] private InventoryItem arrow = default;
    // ^ Shouldn't reassign during runtime unless
    // care is taken to unsubscribe from events.

    private void OnEnable() => arrow.OnNumberHeldChanged += UpdateUI;
    private void OnDisable() => arrow.OnNumberHeldChanged -= UpdateUI;

    private void Start() => UpdateUI();

    private void UpdateUI() => ArrowDisplay.text = "" + arrow.numberHeld;
}
