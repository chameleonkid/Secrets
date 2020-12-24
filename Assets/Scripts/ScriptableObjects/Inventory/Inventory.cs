using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Player Inventory")]
public class Inventory : ScriptableObject
{
    public List<Item> contents = new List<Item>(); //Why is this not loaded???
    public int coins;

    public Item currentItem;
    public InventoryWeapon currentWeapon;
    public InventoryArmor currentArmor;
    public InventoryHelmet currentHelmet;
    public InventoryGlove currentGloves;
    public InventoryLegs currentLegs;
    public InventoryShield currentShield;
    public InventoryRing currentRing;
    public InventoryBow currentBow;
    public InventorySpellbook currentSpellbook;
    public InventoryAmulet currentAmulet;
    public InventoryBoots currentBoots;

    public float totalDefense;
    public float totalCritChance;
    public int totalMinSpellDamage;
    public int totalMaxSpellDamage;

    public void Add(Item item)
    {
        if (!contents.Contains(item))    // Add the item to the list if it is not already in the list.
        {
            contents.Add(item);
        }

        if (item.unique)                    // Force unique items to have `numberHeld = 1`
        {
            item.numberHeld = 1;
        }
        else                                // Regular items have `numberHeld` incremented by 1
        {
            item.numberHeld++;
        }
    }

    public bool Subtract(Item item, int count)
    {
        if (contents.Contains(item) && item.numberHeld >= count)
        {
            item.numberHeld -= count;
            return true;
        }
        else return false;
    }

    public void Equip(Item item)
    {
        switch (item)
        {
            default:
                // Exit the function early if item is not equippable.
                return;
            case InventoryWeapon weapon:
                Swap(ref currentWeapon, weapon);
                break;
            case InventoryArmor armor:
                Swap(ref currentArmor, armor);
                break;
            case InventoryHelmet helmet:
                Swap(ref currentHelmet, helmet);
                break;
            case InventoryGlove gloves:
                Swap(ref currentGloves, gloves);
                break;
            case InventoryLegs legs:
                Swap(ref currentLegs, legs);
                break;
            case InventoryShield shield:
                Swap(ref currentShield, shield);
                break;
            case InventoryRing ring:
                Swap(ref currentRing, ring);
                break;
            case InventoryBow bow:
                Swap(ref currentBow, bow);
                break;
            case InventorySpellbook spellbook:
                Swap(ref currentSpellbook, spellbook);
                break;
            case InventoryAmulet amulet:
                Swap(ref currentAmulet, amulet);
                break;
            case InventoryBoots boots:
                Swap(ref currentBoots, boots);
                break;
        }
        // Applies to all equippables.
        CalcDefense();
        CalcCritChance();
        CalcSpellDamage();
    }

    private void Swap<T>(ref T currentlyEquipped, T newEquip) where T : EquippableItem
    {
        if (currentlyEquipped != null)
        {
            Add(currentlyEquipped);
        }

        currentlyEquipped = newEquip;
        newEquip.numberHeld--;

        if (newEquip.itemSound != null)
        {
            SoundManager.RequestSound(newEquip.itemSound);
        }
    }

    public void CalcDefense()
    {
        totalDefense = 0;

        if (currentArmor)
        {
            totalDefense += currentArmor.armorDefense;
        }
        if (currentHelmet)
        {
            totalDefense += currentHelmet.armorDefense;
        }
        if (currentGloves)
        {
            totalDefense += currentGloves.armorDefense;
        }
        if (currentLegs)
        {
            totalDefense += currentLegs.armorDefense;
        }
        if (currentShield)
        {
            totalDefense += currentShield.armorDefense;
        }
        if (currentBoots)
        {
            totalDefense += currentBoots.armorDefense;
        }
    }

    public void CalcCritChance()
    {
        totalCritChance = 0;

        if (currentRing)
        {
            totalCritChance += currentRing.criticalStrikeChance;
        }
    }

    public void CalcSpellDamage()               //Add other items in the same way to add the min/max amount
    {
        totalMinSpellDamage = 0;
        totalMaxSpellDamage = 0;

        if (currentSpellbook)
        {
            totalMinSpellDamage += currentSpellbook.minSpellDamage;
            totalMaxSpellDamage += currentSpellbook.maxSpellDamage;
        }
        if (currentAmulet)
        {
            totalMinSpellDamage += currentAmulet.minSpellDamage;
            totalMaxSpellDamage += currentAmulet.maxSpellDamage;
        }
    }
}

