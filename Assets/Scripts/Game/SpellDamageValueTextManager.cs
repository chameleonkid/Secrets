using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpellDamageValueTextManager : MonoBehaviour
{

    public PlayerInventory playerInventory;
    public TextMeshProUGUI SpellDamageDisplay;

    private void Start()
    {
        UpdateSpellDamageValue();
    }


    public void UpdateSpellDamageValue()
    {
        if (playerInventory.currentSpellbook)
        {
            SpellDamageDisplay.text = "" + playerInventory.totalSpellDamage;
        }
        else
        {
            SpellDamageDisplay.text = "0";
        }
    }

    private void OnEnable()
    {
        UpdateSpellDamageValue();
    }

}

