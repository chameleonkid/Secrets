using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Bow")]
public class InventoryBow : InventoryItem
{
    public int minDamage;
    public int maxDamage;

    public void swapBow()
    {
        if (myInventory.currentBow)
        {
            // Return currently equipped bow to inventory
            myInventory.Add(myInventory.currentBow);
        }
        
        myInventory.equip(this);
        this.numberHeld--;
        if (itemSound)
        {
            SoundManager.RequestSound(itemSound);
        }
    }
}
