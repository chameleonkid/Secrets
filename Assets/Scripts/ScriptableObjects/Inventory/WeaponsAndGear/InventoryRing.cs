using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Rings")]
public class InventoryRing : InventoryItem
{
    public int criticalStrikeChance;

    public void swapRing()
    {
        if (myInventory.currentRing)
        {
            // Return currently equipped ring to inventory
            myInventory.Add(myInventory.currentRing);
        }

        myInventory.equip(this);
        this.numberHeld--;
        if (itemSound)
        {
            SoundManager.RequestSound(itemSound);
        }
    }
}
