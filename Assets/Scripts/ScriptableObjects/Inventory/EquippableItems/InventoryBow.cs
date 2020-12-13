using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Bow")]
public class InventoryBow : EquippableItem
{
    public int minDamage;
    public int maxDamage;

    public override string fullDescription
        => itemDescription + ("\n\n DMG: ") + minDamage + " - " + maxDamage;
}
