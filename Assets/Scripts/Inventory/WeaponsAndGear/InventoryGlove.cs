using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Gloves")]
public class InventoryGlove : InventoryItem
{
    public int armorDefense;

    private InventoryGlove temp;

    public void swapGlove()
    {
        if (myInventory.currentGloves)
        {
            temp = myInventory.currentGloves;
            myInventory.Add(myInventory.currentGloves);
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
