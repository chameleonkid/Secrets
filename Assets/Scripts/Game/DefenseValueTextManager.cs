using UnityEngine;
using TMPro;

public class DefenseValueTextManager : MonoBehaviour
{
    public Inventory playerInventory;
    public TextMeshProUGUI defenseDisplay;

    public void UpdateDefenseValue()
    {
        if (playerInventory.totalDefense > 0)
        {
            defenseDisplay.text = "" + playerInventory.totalDefense;
        }
        else
        {
            defenseDisplay.text = "";
        }
    }

    private void OnEnable()
    {
        UpdateDefenseValue();
    }
}
