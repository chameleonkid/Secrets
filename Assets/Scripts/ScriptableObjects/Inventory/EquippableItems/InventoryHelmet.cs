using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Items/Helmet")]
public class InventoryHelmet : EquippableItem
{
    public int armorDefense;

    public override string fullDescription
        => description + ("\n\n ARMOR: ") + armorDefense;
}
