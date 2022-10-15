using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Items/Spellbook/AOESpell")]
public class AOESpellbook : InventorySpellbook
{
    public int minSpellDamage;
    public int maxSpellDamage;

    public GameObject prefab;
    public float radius = 1;

    public override string fullDescription
        => description + ("\n\n SPELL-DMG: ") + minSpellDamage + " - " + maxSpellDamage + ("\n\n Level: ") + level;
}
