using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Shields")]
public class InventoryShield : EquippableItem
{
    public int armorDefense;

    public override string fullDescription
        => description + ("\n\n ARMOR: ") + armorDefense;
}
