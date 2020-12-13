using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Spellbook")]
public class InventorySpellbook : EquippableItem
{
    public int minSpellDamage;
    public int maxSpellDamage;

    public int manaCosts;
    public GameObject prefab;
    public float speed = 1;

    public override string fullDescription
        => itemDescription + ("\n\n SPELL-DMG: ") + minSpellDamage + " - " + maxSpellDamage;
}
