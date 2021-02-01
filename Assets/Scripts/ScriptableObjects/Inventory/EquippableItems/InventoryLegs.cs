using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Items/Legs")]
public class InventoryLegs : EquippableItem
{
    public int armorDefense;

    public override string fullDescription
        => description + ("\n\n ARMOR: ") + armorDefense + ("\n\n Level: ") + level;
}
