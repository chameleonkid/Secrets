using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Armors")]
public class InventoryArmor : InventoryItem
{
    public int armorDefense;

    public void swapArmor()
    {
        if (myInventory.currentArmor)
        {
            // Return currently equipped armor to inventory
            myInventory.Add(myInventory.currentArmor);
        }

        myInventory.equip(this);
        this.numberHeld--;
        if (itemSound)
        {
            SoundManager.RequestSound(itemSound);
        }
    }
}
