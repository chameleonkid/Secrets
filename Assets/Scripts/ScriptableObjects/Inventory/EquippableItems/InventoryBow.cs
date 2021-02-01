using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Items/Bow")]
public class InventoryBow : EquippableItem
{
    public int minDamage;
    public int maxDamage;

    public override string fullDescription
        => description + ("\n\n DMG: ") + minDamage + " - " + maxDamage + ("\n\n Level: ") + level;
}
