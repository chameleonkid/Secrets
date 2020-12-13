using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Gloves")]
public class InventoryGlove : EquippableItem
{
    public int armorDefense;

    public override string fullDescription
        => itemDescription + ("\n\n ARMOR: ") + armorDefense;
}
