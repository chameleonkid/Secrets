using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Weapons")]
public class InventoryWeapon : InventoryItem
{
    public int minDamage;
    public int maxDamage;

    public void swapWeapon()
    {
        if (myInventory.currentWeapon)
        {
            // Return currently equipped weapon to inventory
            myInventory.Add(myInventory.currentWeapon);
        }

        myInventory.equip(this);
        this.numberHeld--;
        if (itemSound)
        {
            SoundManager.RequestSound(itemSound);
        }
    }
}
