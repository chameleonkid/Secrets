using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Bow")]
public class InventoryBow : InventoryItem
{
    public int minDamage;
    public int maxDamage;

    private InventoryBow temp;

    public void swapBow()
    {
        if (myInventory.currentBow)
        {
            temp = myInventory.currentBow;
            myInventory.Add(myInventory.currentBow);
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
