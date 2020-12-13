using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Weapons")]
public class InventoryWeapon : EquippableItem
{
    public int minDamage;
    public int maxDamage;

    public override string fullDescription
        => itemDescription + ("\n\n DMG: ") + minDamage + " - " + maxDamage;
}
