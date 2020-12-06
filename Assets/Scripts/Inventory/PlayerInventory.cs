using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Player Inventory")]
public class PlayerInventory : ScriptableObject
{
    public List<InventoryItem> myInventory = new List<InventoryItem>(); //Why is this not loaded???
    public int coins;
    public InventoryItem currentItem;
    public InventoryWeapon currentWeapon;
    public InventoryArmor currentArmor;
    public InventoryHelmet currentHelmet;
    public InventoryGlove currentGloves;
    public InventoryLegs currentLegs;
    public InventoryShield currentShield;
    public InventoryRing currentRing;
    public InventoryBow currentBow;
    public float totalDefense;
    public float totalCritChance;
    public float maxMana = 100;
    public float currentMana = 100;

    public void Add(InventoryItem item)
    {
        if (!myInventory.Contains(item))    // Add the item to the list if it is not already in the list.
        {
            myInventory.Add(item);
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

    public void equip(InventoryItem item)
    {
        switch (item)
        {
            default:
                // Exit the function early if item is not equippable.
                return;
            case InventoryWeapon weapon:
                currentWeapon = weapon;
                break;
            case InventoryArmor armor:
                currentArmor = armor;
                break;
            case InventoryHelmet helmet:
                currentHelmet = helmet;
                break;
            case InventoryGlove glove:
                currentGloves = glove;
                break;
            case InventoryLegs legs:
                currentLegs = legs;
                break;
            case InventoryShield shield:
                currentShield = shield;
                break;
            case InventoryRing ring:
                currentRing = ring;
                break;
            case InventoryBow bow:
                currentBow = bow;
                break;
        }
        // Applies to all equippables.
        item.numberHeld = 1;
        calcDefense();
        CalcCritChance();
    }

    public void calcDefense()
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
    }

    public void CalcCritChance()
    {
        totalCritChance = 0;

        if (currentRing)
        {
            totalCritChance += currentRing.criticalStrikeChance;
        }
    }


    // Added to manipulate Mana in Inventory from PlayerMovement

    public void DecreaseCurrentMana(int amountToDecrease)
    {
        if(currentMana - amountToDecrease <= 0)
        {
            currentMana = 0;
        }
        else
        {
            currentMana -= amountToDecrease;
        }
    }

    public void IncreaseCurrentMana(int amountToIncrease)
    {
        if (currentMana + amountToIncrease > maxMana)
        {
            currentMana = maxMana;
        }
        else
        {
            currentMana += amountToIncrease;
        }
    }
}
