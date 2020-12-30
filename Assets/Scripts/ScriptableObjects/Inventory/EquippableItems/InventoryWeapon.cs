using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Items/Weapon")]
public class InventoryWeapon : EquippableItem
{
    public int minDamage;
    public int maxDamage;

    public override string fullDescription
        => description + ("\n\n DMG: ") + minDamage + " - " + maxDamage;
}
