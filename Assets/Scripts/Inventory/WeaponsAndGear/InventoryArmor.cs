using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Armors")]
public class InventoryArmor : InventoryItem
{
    public int armorDefense;

    private InventoryArmor temp;


    public void swapArmor()
    {
        if (myInventory.currentArmor)
        {
            temp = myInventory.currentArmor;
            myInventory.Add(myInventory.currentArmor);
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
