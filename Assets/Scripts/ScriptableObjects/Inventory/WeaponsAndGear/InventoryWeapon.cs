using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Weapons")]
public class InventoryWeapon : InventoryItem
{
    public int minDamage;
    public int maxDamage;

    public void swapWeapon() => myInventory.equip(this);
}
