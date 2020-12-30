using UnityEngine;
using TMPro;

public class CoinTextManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinDisplay = default;
    [SerializeField] private Inventory _inventory = default;
    public Inventory inventory {
        get => _inventory;
        set {
            if (_inventory != value) {
                if (_inventory != null) {
                    _inventory.OnCoinCountChanged -= UpdateDisplay;
                }

                if (value != null) {
                    value.OnCoinCountChanged += UpdateDisplay;
                }

                _inventory = value;
            }
        }
    }

    private void OnEnable()
    {
        if (inventory != null)
        {
            inventory.OnCoinCountChanged += UpdateDisplay;
        }

        UpdateDisplay();
    }

    private void OnDisable()
    {
        if (inventory != null)
        {
            inventory.OnCoinCountChanged -= UpdateDisplay;
        }
    }

    private void UpdateDisplay() => coinDisplay.text = inventory.coins.ToString();
}
