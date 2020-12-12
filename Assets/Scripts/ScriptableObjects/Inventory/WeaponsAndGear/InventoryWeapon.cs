using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Weapons")]
public class InventoryWeapon : InventoryItem
{
    public int minDamage;
    public int maxDamage;
    private InventoryWeapon temp;

    public void swapWeapon()
    {
        if (myInventory.currentWeapon)
        {
            temp = myInventory.currentWeapon;
            myInventory.Add(myInventory.currentWeapon);
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

