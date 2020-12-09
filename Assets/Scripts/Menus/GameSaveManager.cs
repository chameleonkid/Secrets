using System.Collections.Generic;
using UnityEngine;

public class GameSaveManager : MonoBehaviour
{
    public static GameSaveManager gameSave;

    //################# Things to Reset ############################
    public List<BoolValue> chests = new List<BoolValue>();
    public List<BoolValue> doors = new List<BoolValue>();
    public List<InventoryItem> ResetInventoryItems = new List<InventoryItem>();
    public PlayerInventory playerInventory;

    //################# Refresh the Screen ############################;
    public Signals coinSignal;

    //################# Every Scriptable Object needs to be touched ###############
    public InventoryItem bluePotion;
    public InventoryItem yellowPotion;
    public InventoryItem greenPotion;
    public InventoryItem redPotion;
    public InventoryItem arrows;
    public InventoryItem smallKeys;

    public VectorValue playerPosition;
    public Transform currentPlayerPosition;

    public FloatMeter health;
    public FloatMeter mana;


    public void ResetScriptables()
    {
        for (int i = 0; i < chests.Count; i++)
        {
            chests[i].RuntimeValue = chests[i].initialValue;
            
        }
        for (int i = 0; i < doors.Count; i++)
        {
            doors[i].RuntimeValue = doors[i].initialValue;
        }

        resetHealthAndMana();
        resetInventory();
        Debug.Log("RESET!");
    }

    //################################################################# RESET EVERYTHING ###################################################################
    public void resetInventory()
    {
        playerInventory.coins = 0;

        for (int i = 0; i < playerInventory.myInventory.Count; i++)
        {
            playerInventory.myInventory[i].numberHeld = 0;
        }

        playerInventory.currentItem = null;
        playerInventory.currentWeapon = null;
        playerInventory.currentArmor = null;
        playerInventory.currentHelmet = null;
        playerInventory.currentGloves = null;
        playerInventory.currentLegs = null;
        playerInventory.currentShield = null;
        playerInventory.currentRing = null;
        playerInventory.currentBow = null;
        playerInventory.currentSpellbook = null;

        playerInventory.totalDefense = 0;
        playerInventory.totalCritChance = 0;

        playerInventory.myInventory.Clear();   //  somehow my items reset to different values when this is done...When Watched in Inspector its fine 

        // Look at this https://answers.unity.com/questions/1501743/scriptable-object-resets-randomly-between-scenes.html);
        coinSignal.Raise();
    }

    public void resetHealthAndMana()
    {
        health.max = 10;
        health.current = health.max;
        mana.max = 100;
        mana.current = mana.max;
    }
}
