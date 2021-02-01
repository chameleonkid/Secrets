using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Items/Gloves")]
public class InventoryGlove : EquippableItem
{
    public int armorDefense;

    public override string fullDescription
        => description + ("\n\n ARMOR: ") + armorDefense + ("\n\n Level: ") + level;
}
