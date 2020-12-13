using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Armors")]
public class InventoryArmor : EquippableItem
{
    public int armorDefense;

    public override string fullDescription
        => itemDescription + ("\n\n ARMOR: ") + armorDefense;
}
