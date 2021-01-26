using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Items/Shoulders")]
public class InventoryShoulder : EquippableItem

{
    public int armorDefense;
    public int criticalStrikeChance;

    public override string fullDescription
        => description + ("\n\n ARMOR: ") + armorDefense + ("\n\n CRIT: ") + criticalStrikeChance;
}

