using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Boots")]
public class InventoryBoots : EquippableItem
{
    public int armorDefense;

    public override string fullDescription
        => itemDescription + ("\n\n ARMOR: ") + armorDefense;
}
