using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Rings")]
public class InventoryRing : EquippableItem
{
    public int criticalStrikeChance;

    public override string fullDescription
        => itemDescription + ("\n\n CRITICAL STRIKE CHANCE: ") + criticalStrikeChance + ("%");
}
