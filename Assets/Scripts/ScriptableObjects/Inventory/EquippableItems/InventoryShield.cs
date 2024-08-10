using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Items/Shield")]
public class InventoryShield : EquippableItem
{
    public int armorDefense;
    public Sprite shieldSpriteUp;
    public Sprite shieldSpriteDown;
    public Sprite shieldSpriteLeft;
    public Sprite shieldSpriteRight;

    public override string fullDescription
        => description + ("\n\n ARMOR: ") + armorDefense + ("\n\n Level: ") + level;
}
