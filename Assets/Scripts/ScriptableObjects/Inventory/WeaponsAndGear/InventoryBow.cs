using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Bow")]
public class InventoryBow : InventoryItem
{
    public int damage;

    private InventoryBow temp;

    public void swapBow()
    {
        if (myInventory.currentBow)
        {
            temp = myInventory.currentBow;
            myInventory.Add(myInventory.currentBow);
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
