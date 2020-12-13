using UnityEngine;
using TMPro;

public class CritValueTextManager : MonoBehaviour
{
    public Inventory playerInventory;
    public TextMeshProUGUI critDisplay;

    private void Start()
    {
        UpdateCritValue();
    }

    public void UpdateCritValue()
    {
        if (playerInventory.totalCritChance > 0)
        {
            critDisplay.text = "" + playerInventory.totalCritChance + ("%");
        }
        else
        {
            critDisplay.text = "";
        }
    }

    private void OnEnable()
    {
        UpdateCritValue();
    }
}
