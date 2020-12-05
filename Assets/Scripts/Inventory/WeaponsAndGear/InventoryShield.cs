using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Shields")]
public class InventoryShield : InventoryItem
{
    public int armorDefense;
    private InventoryShield temp;

    public void swapShield()
    {
        if (myInventory.currentShield)
        {
            temp = myInventory.currentShield;
            myInventory.Add(myInventory.currentShield);
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
