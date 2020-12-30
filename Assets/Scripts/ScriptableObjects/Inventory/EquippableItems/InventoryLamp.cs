using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Items/Lamp")]
public class InventoryLamp : EquippableItem
{
    public int innerRadius;
    public int outerRadius;
    public Color color;

    public override string fullDescription
        => description + ("\n\n Lightradius: ") + outerRadius;
}
