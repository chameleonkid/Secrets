using UnityEngine;
using TMPro;

public class ArrowManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ArrowDisplay = default;
    [SerializeField] private Item arrow = default;
    [SerializeField] private Inventory inventory = default;
    // ^ Shouldn't reassign during runtime unless
    // care is taken to unsubscribe from events.

    private void OnEnable() => inventory.items.OnContentsChanged += UpdateUI;
    private void OnDisable() => inventory.items.OnContentsChanged -= UpdateUI;

    private void Start() => UpdateUI(null, 0);

    private void UpdateUI(Item item, int count) => ArrowDisplay.text = inventory.items[arrow].ToString();
}
