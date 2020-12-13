using UnityEngine;
using TMPro;

public class SpellDamageValueTextManager : MonoBehaviour
{
    public Inventory playerInventory;
    public TextMeshProUGUI SpellDamageDisplay;

    private void Start()
    {
        UpdateSpellDamageValue();
    }

    public void UpdateSpellDamageValue()
    {
        if (playerInventory.currentSpellbook || playerInventory.currentAmulet)
        {
            SpellDamageDisplay.text =  playerInventory.totalMinSpellDamage + " - "  + playerInventory.totalMaxSpellDamage;
        }
        else
        {
            SpellDamageDisplay.text = "";
        }
    }

    private void OnEnable()
    {
        UpdateSpellDamageValue();
    }
}
