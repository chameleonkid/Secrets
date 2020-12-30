using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Items/Armor")]
public class InventoryArmor : EquippableItem
{
    public int armorDefense;

    public override string fullDescription
        => description + ("\n\n ARMOR: ") + armorDefense;
}
