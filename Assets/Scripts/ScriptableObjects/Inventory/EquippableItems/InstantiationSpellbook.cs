using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Items/Spellbook/Instantiation Spell")]
public class InstantiationSpellbook : InventorySpellbook
{
    public int minSpellDamage;
    public int maxSpellDamage;

    public GameObject prefab;
    public float speed = 1;

    public override string fullDescription
        => description + ("\n\n SPELL-DMG: ") + minSpellDamage + " - " + maxSpellDamage + ("\n\n Level: ") + level;
}
