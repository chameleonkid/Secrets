using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Helmets")]
public class InventoryHelmet : InventoryItem
{
    public int armorDefense;

    private InventoryHelmet temp;

    public void swapHelmet()
    {
        if (myInventory.currentHelmet)
        {
            temp = myInventory.currentHelmet;
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
