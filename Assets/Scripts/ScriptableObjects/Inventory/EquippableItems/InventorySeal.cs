using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Items/Seal")]
public class InventorySeal : EquippableItem

{
    public int armorDefense;
    public int criticalStrikeChance;

    public override string fullDescription
        => description + ("\n\n ARMOR: ") + armorDefense + ("\n\n CRIT: ") + criticalStrikeChance;
}

