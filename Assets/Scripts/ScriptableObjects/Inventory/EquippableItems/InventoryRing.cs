using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Items/Ring")]
public class InventoryRing : EquippableItem
{
    public int criticalStrikeChance;

    public override string fullDescription
        => description + ("\n\n CRITICAL STRIKE CHANCE: ") + criticalStrikeChance + ("%");
}
