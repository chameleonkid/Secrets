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

    // ####################################### add new Item / Weapon / Armor to inventory ######################################
    public void Add(InventoryItem item)
    {
        //Since its nothing else it must be a regular Item
        {
            if (item.unique)                                                //Is the item Unique?                
            {
                if (!myInventory.Contains(item) && item is InventoryItem)   // Is item NOT in Inventory? and a regular Item? Add it!
                {
                    myInventory.Add(item);
                    // item.numberHeld++; Änderung 05_12_2020
                    item.numberHeld = 1;
                }
                else                                                        // The item is unique and is already in the inventory? set numberheld to 1 since its unique
                {
                    item.numberHeld = 1;
                }
            }
            else                                                            // Item is regular and not unique
            {
                if (myInventory.Contains(item))                              //Item is regular and not unique and IS ALREADY in the inventory
                {
                    item.numberHeld++;
                }
                else                                                        //Item is regular and not unique and IS NOT in the inventory
                {
                    myInventory.Add(item);
                    item.numberHeld++;
                }
            }
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
        /*
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
            if (currentArmor)
    {
        totalDefense += currentArmor.armorDefense;
    }
    */
        if (currentRing)
        {
            totalCritChance += currentRing.criticalStrikeChance;
        }
    }
}
