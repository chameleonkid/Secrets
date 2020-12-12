using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Legs")]
public class InventoryLegs : InventoryItem
{
    public int armorDefense;

    public void swapLegs()
    {
        if (myInventory.currentLegs)
        {
            // Return currently equipped legs to inventory
            myInventory.Add(myInventory.currentLegs);
        }

        myInventory.equip(this);
        this.numberHeld--;
        if (itemSound)
        {
            SoundManager.RequestSound(itemSound);
        }
    }
}
