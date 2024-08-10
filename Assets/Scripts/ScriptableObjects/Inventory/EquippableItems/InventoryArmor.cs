using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Items/Armor")]
public class InventoryArmor : EquippableItem
{
    public int armorDefense;

    // Reference to the texture used for this armor
    public Texture2D texture; // The texture to represent the armor visually

    public override string fullDescription
        => description + "\n\nARMOR: " + armorDefense + "\n\nLevel: " + level;
}