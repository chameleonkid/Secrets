using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Items/Spellbook")]
public class InstantiationSpellbook : EquippableItem
{


    public int minSpellDamage;
    public int maxSpellDamage;
    public float coolDown;
    public bool onCooldown = false;
    [ColorUsageAttribute(true, true)] public Color glowColor;

    public int manaCosts;
    public GameObject prefab;
    public float speed = 1;

    public override string fullDescription
        => description + ("\n\n SPELL-DMG: ") + minSpellDamage + " - " + maxSpellDamage + ("\n\n Level: ") + level;

    private void OnEnable()
    {
        onCooldown = false;
    }
}
