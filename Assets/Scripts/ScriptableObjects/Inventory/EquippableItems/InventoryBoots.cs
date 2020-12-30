using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Items/Boots")]
public class InventoryBoots : EquippableItem
{
    public int armorDefense;

    public override string fullDescription
        => description + ("\n\n ARMOR: ") + armorDefense;
}
