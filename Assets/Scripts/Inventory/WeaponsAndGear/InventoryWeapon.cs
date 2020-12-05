using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Weapons")]
public class InventoryWeapon : InventoryItem
{
    public int damage;
    private InventoryWeapon temp;


    public void swapWeapon()
    {
        if (myInventory.currentWeapon)
        {
            temp = myInventory.currentWeapon;
            myInventory.Add(myInventory.currentWeapon);
            myInventory.equip(this);
            this.numberHeld--;
        }
        else
        {
            myInventory.equip(this);
            this.numberHeld--;
        }

    }

}

