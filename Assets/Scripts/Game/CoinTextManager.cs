using UnityEngine;
using TMPro;

public class CoinTextManager : MonoBehaviour
{
    public Inventory playerInventory;
    public TextMeshProUGUI coinDisplay;

    private void Start()
    {
        UpdateCoinCount();
    }

    private void OnEnable()
    {
        UpdateCoinCount();
    }

    public void UpdateCoinCount()
    {
        coinDisplay.text = "" + playerInventory.coins;
    }
}
