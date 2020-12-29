using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Gloves")]
public class InventoryGlove : EquippableItem
{
    public int armorDefense;

    public override string fullDescription
        => description + ("\n\n ARMOR: ") + armorDefense;
}
