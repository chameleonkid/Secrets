using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Amulet")]
public class InventoryAmulet : InventoryItem
{
    public int minSpellDamage;
    public int maxSpellDamage;
    private InventoryAmulet temp;




    public void swapAmulet()
    {
        if (myInventory.currentAmulet)
        {
            temp = myInventory.currentAmulet;
            myInventory.Add(myInventory.currentAmulet);
            myInventory.equip(this);
            this.numberHeld--;
            if (itemSound)
            {
                SoundManager.RequestSound(itemSound);
            }
        }
        else
        {
            myInventory.equip(this);
            this.numberHeld--;
            if (itemSound)
            {
                SoundManager.RequestSound(itemSound);
            }
        }
    }
}

