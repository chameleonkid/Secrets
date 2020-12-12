using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Gloves")]
public class InventoryGlove : InventoryItem
{
    public int armorDefense;

    public void swapGlove()
    {
        if (myInventory.currentGloves)
        {
            // Return currently equipped gloves to inventory
            myInventory.Add(myInventory.currentGloves);
        }
        
        myInventory.equip(this);
        this.numberHeld--;
        if (itemSound)
        {
            SoundManager.RequestSound(itemSound);
        }
    }
}
