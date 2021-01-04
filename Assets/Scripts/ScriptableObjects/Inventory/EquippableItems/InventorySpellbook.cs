using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Items/Spellbook")]
public class InventorySpellbook : EquippableItem
{
    public int minSpellDamage;
    public int maxSpellDamage;
    public float coolDown;

    public int manaCosts;
    public GameObject prefab;
    public float speed = 1;

    public override string fullDescription
        => description + ("\n\n SPELL-DMG: ") + minSpellDamage + " - " + maxSpellDamage;
}
