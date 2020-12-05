﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Items")]
public class InventoryItem : ScriptableObject
{
    public string itemName;
    public string itemDescription;
    public Sprite itemImage;
    public int numberHeld;
    public bool usable;
    public bool unique;
    public UnityEvent thisEvent;
    public int itemLvl;
    public PlayerInventory myInventory;


    public void Use()
    {
            thisEvent.Invoke();
    }
    public void decreaseAmount(int amountToDecrease)
    {
        numberHeld-=amountToDecrease;
        if (numberHeld<0)
        {
            numberHeld = 0;
        }
    }
}
