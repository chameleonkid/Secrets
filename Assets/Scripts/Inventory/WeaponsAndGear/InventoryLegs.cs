using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Legs")]
public class InventoryLegs : InventoryItem
{
    public int armorDefense;

    private InventoryLegs temp;

    public void swapLegs()
    {
        if (myInventory.currentLegs)
        {
            temp = myInventory.currentLegs;
            myInventory.Add(myInventory.currentLegs);
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
