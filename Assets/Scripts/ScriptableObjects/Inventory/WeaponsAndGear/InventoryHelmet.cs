using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Helmets")]
public class InventoryHelmet : InventoryItem
{
    public int armorDefense;

    public void swapHelmet()
    {
        if (myInventory.currentHelmet)
        {
            // Return currently equipped helmet to inventory
            myInventory.Add(myInventory.currentHelmet);
        }
        
        myInventory.equip(this);
        this.numberHeld--;
        if (itemSound)
        {
            SoundManager.RequestSound(itemSound);
        }
    }
}
