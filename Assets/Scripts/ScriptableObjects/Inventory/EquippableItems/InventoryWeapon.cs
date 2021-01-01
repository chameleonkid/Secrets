using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Items/Weapon")]
public class InventoryWeapon : EquippableItem
{
    public int minDamage;
    public int maxDamage;
    public float glowIntensity;
    [ColorUsageAttribute(true, true)] public Color glowColor;



    public override string fullDescription
        => description + ("\n\n DMG: ") + minDamage + " - " + maxDamage;

}
