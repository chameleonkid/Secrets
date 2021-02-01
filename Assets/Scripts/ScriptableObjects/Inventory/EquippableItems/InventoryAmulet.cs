using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Items/Amulet")]
public class InventoryAmulet : EquippableItem
{
    public int minSpellDamage;
    public int maxSpellDamage;

    public override string fullDescription
        => description + ("\n\n SPELL-DMG: ") + minSpellDamage + " - " + maxSpellDamage + ("\n\n Level: ") + level;
}
