using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Amulet")]
public class InventoryAmulet : InventoryItem
{
    public int minSpellDamage;
    public int maxSpellDamage;

    public void swapAmulet()
    {
        if (myInventory.currentAmulet)
        {
            // Return equipped amulet to inventory
            myInventory.Add(myInventory.currentAmulet);
        }
        
        myInventory.equip(this);
        this.numberHeld--;
        if (itemSound)
        {
            SoundManager.RequestSound(itemSound);
        }
    }
}
