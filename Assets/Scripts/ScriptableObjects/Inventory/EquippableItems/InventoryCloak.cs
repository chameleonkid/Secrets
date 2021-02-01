using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Items/Cloak")]
public class InventoryCloak : EquippableItem
{
    public int minSpellDamage;
    public int maxSpellDamage;
    public int armorDefense;
    public int criticalStrikeChance;

    public override string fullDescription
        => description + ("\n\n SPELL-DMG: ") + minSpellDamage + " - " + maxSpellDamage + ("\n\n ARMOR: ") + armorDefense + ("\n\n CRIT: ") + criticalStrikeChance + ("\n\n Level: ") + level;
}
