using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Rings")]
public class InventoryRing : InventoryItem
{
    public int criticalStrikeChance;

    private InventoryRing temp;

    public void swapRing()
    {
        if (myInventory.currentRing)
        {
            temp = myInventory.currentRing;
            myInventory.Add(myInventory.currentRing);
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
