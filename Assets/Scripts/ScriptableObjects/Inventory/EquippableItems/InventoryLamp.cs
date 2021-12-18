using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Items/Lamp")]
public class InventoryLamp : EquippableItem
{
    public float innerRadius;
    public float outerRadius;
    public Color color;
    public int lumenPerSecond;

    public override string fullDescription
        => description + ("\n\n Lightradius: ") + outerRadius + ("\n\n Level: ") + level;
}
