using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Helmets")]
public class InventoryHelmet : EquippableItem
{
    public int armorDefense;

    public override string fullDescription
        => itemDescription + ("\n\n ARMOR: ") + armorDefense;
}
