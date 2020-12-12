using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RangeDamageValueTextManager : MonoBehaviour
{

    public PlayerInventory playerInventory;
    public TextMeshProUGUI RangeDamageDisplay;

    private void Start()
    {
        UpdateRangeDamageValue();
    }


    public void UpdateRangeDamageValue()
    {
        if (playerInventory.currentBow) // All items with RangeDMG seperated by || 
        {
            RangeDamageDisplay.text = playerInventory.currentBow.minDamage + " - " + playerInventory.currentBow.maxDamage;
        }
        else
        {
            RangeDamageDisplay.text = "";
        }
    }

    private void OnEnable()
    {
        UpdateRangeDamageValue();
    }

}

