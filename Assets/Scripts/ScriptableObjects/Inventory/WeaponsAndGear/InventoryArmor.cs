using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Armors")]
public class InventoryArmor : InventoryItem
{
    public int armorDefense;

    private InventoryArmor temp;

    public void swapArmor()
    {
        if (myInventory.currentArmor)
        {
            temp = myInventory.currentArmor;
            myInventory.Add(myInventory.currentArmor);
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
