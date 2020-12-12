using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageValueTextManager : MonoBehaviour
{

    public PlayerInventory playerInventory;
    public TextMeshProUGUI damageDisplay;

    private void Start()
    {
        UpdateDamageValue();
    }


    public void UpdateDamageValue()
    {
        if (playerInventory.currentWeapon)
        {
            damageDisplay.text = "" + playerInventory.currentWeapon.damage;
        }
        else
        {
            damageDisplay.text = "0";
        }
    }

    private void OnEnable()
    {
        UpdateDamageValue();
    }

}

