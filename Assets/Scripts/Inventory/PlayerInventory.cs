using System.Collections;
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
                if(myInventory.Contains(item))                              //Item is regular and not unique and IS ALREADY in the inventory
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
        //################################################################### New Version #################################################

        //  if (item is InventoryWeapon && (currentWeapon == null || item.itemLvl >= currentWeapon.itemLvl))           If Level is higher Replace                             
        if (item is InventoryWeapon)
        {
            currentWeapon = item as InventoryWeapon;                        //declare as Weapon
            item.numberHeld = 1;                                            //Weapons are always Unique so 1
        }
        else if (item is InventoryArmor)                                    //Is Item an Armor?
        {
            currentArmor = item as InventoryArmor;                          //declare as Armor
            item.numberHeld = 1;                                            //Armors are always Unique so 1
            calcDefense();                                                  //calc new defenseValue
            CalcCritChance();
        }
        else if (item is InventoryHelmet)                                    //Is Item a Helmet?
        {
            currentHelmet = item as InventoryHelmet;                        //declare as Helmet
            item.numberHeld = 1;                                            //Helmets are always Unique so 1
            calcDefense();                                                  //calc new defenseValue
            CalcCritChance();
        }
        else if (item is InventoryGlove)                                    //Is Item a Glove?
        {
            currentGloves = item as InventoryGlove;                         //declare as Glove
            item.numberHeld = 1;                                            //Gloves are always Unique so 1
            calcDefense();                                                  //calc new defenseValue
            CalcCritChance();
        }
        else if (item is InventoryLegs)                                     //Is Item a Leg?
        {
            currentLegs = item as InventoryLegs;                            //declare as Leg
            item.numberHeld = 1;                                            //Legs are always Unique so 1
            calcDefense();                                                  //calc new defenseValue
            CalcCritChance();
        }
        else if (item is InventoryShield)                                   //Is Item a Shield?
        {
            currentShield = item as InventoryShield;                        //declare as Shield
            item.numberHeld = 1;                                            //Shield are always Unique so 1
            calcDefense();                                                  //calc new defenseValue
            CalcCritChance();
        }
        else if (item is InventoryRing)                                     //Is Item a Ring?
        {
            currentRing = item as InventoryRing;                            //declare as Ring
            item.numberHeld = 1;                                            //Ring are always Unique so 1
            calcDefense();                                                  //calc new defenseValue
            CalcCritChance();
        }
        else if (item is InventoryBow)                                     //Is Item a Ring?
        {
            currentBow = item as InventoryBow;                              //declare as Ring
            item.numberHeld = 1;                                            //Ring are always Unique so 1
            calcDefense();                                                  //calc new defenseValue
            CalcCritChance();
        }


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


