using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Items/Equipment")]
public class Eqipment : InventorySpellbook
{
    public int minSpellDamage;
    public int maxSpellDamage;

    public GameObject prefab;
    public float speed = 1;
    public int amountOfProjectiles = 1;
    public float delayBetweenProjectiles = 0.1f;
    public float spreadAngle;
    public bool groupDirection = true;
    public float radius;
    public bool isRotating;

    public override string fullDescription
        => description + ("\n\n SPELL-DMG: ") + minSpellDamage + " - " + maxSpellDamage + ("\n\n Level: ") + level;
}
