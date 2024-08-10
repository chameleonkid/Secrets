using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Items/Armor")]
public class InventoryArmor : EquippableItem
{
    public int armorDefense;

    // Reference to the texture used for this armor
    public Texture2D textureMale; // The texture to represent the armor visually
    public Texture2D textureFemale; // The texture to represent the armor visually

    public override string fullDescription
        => description + "\n\nARMOR: " + armorDefense + "\n\nLevel: " + level;
}