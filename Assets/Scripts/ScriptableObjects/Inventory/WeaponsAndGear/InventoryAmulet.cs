﻿using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Amulet")]
public class InventoryAmulet : EquippableItem
{
    public int minSpellDamage;
    public int maxSpellDamage;
}
