using UnityEngine;

public abstract class InventorySpellbook : EquippableItem
{
    [SerializeField] private float _manaCost;
    public float manaCost => _manaCost;

    public float coolDown;
    public bool onCooldown = false;
    [ColorUsageAttribute(true, true)] public Color glowColor;

    protected void OnEnable() => onCooldown = false;
}
