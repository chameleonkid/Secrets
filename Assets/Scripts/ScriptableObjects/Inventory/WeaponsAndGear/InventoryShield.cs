using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Shields")]
public class InventoryShield : InventoryItem
{
    public int armorDefense;

    public void swapShield()
    {
        if (myInventory.currentShield)
        {
            // Return currently equipped shield to inventory
            myInventory.Add(myInventory.currentShield);
        }

        myInventory.equip(this);
        this.numberHeld--;
        if (itemSound)
        {
            SoundManager.RequestSound(itemSound);
        }
    }
}
