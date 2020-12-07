using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameSaveManager : MonoBehaviour
{
    public static GameSaveManager gameSave;

    //################# Things to Reset ############################
    public List<BoolValue> chests = new List<BoolValue>();
    public List<BoolValue> doors = new List<BoolValue>();
    public List<InventoryItem> ResetInventoryItems = new List<InventoryItem>();
    public FloatValue HeartContainers;
    public FloatValue playerHealth;
    // public Inventory Inventory;
    public PlayerInventory playerInventory;

    //################# Refresh the Screen ############################
    public Signals heartSignal;
    public Signals arrowSignal;
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

    public void ResetScriptables()
    {
        for (int i = 0; i < chests.Count; i++)
        {
            if (File.Exists("E:\\Secret_Save" + string.Format("/{0}.chests", i)))
            {
                File.Delete("E:\\Secret_Save" + string.Format("/{0}.chests", i));
            }
            chests[i].RuntimeValue = chests[i].initialValue;
        }
        for (int i = 0; i < doors.Count; i++)
        {
            if (File.Exists("E:\\Secret_Save" + string.Format("/{0}.doors", i)))
            {
                File.Delete("E:\\Secret_Save" + string.Format("/{0}.doors", i));
            }
            doors[i].RuntimeValue = doors[i].initialValue;
        }

        resetHealth();
        resetInventory();
        Debug.Log("RESET!");
    }

    //################################################################# RESET EVERYTHING ###################################################################
    public void resetInventory()
    {
        playerInventory.coins = 0;

        for (int i = 0; i < playerInventory.myInventory.Count; i++)
        {

            if (File.Exists("E:\\Secret_Save" + string.Format("/{0}.inventory", i)))
            {
                File.Delete("E:\\Secret_Save" + string.Format("/{0}.inventory", i));
            }
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

        playerInventory.totalDefense = 0;
        playerInventory.totalCritChance = 0;

        playerInventory.myInventory.Clear();   //  somehow my items reset to different values when this is done...When Watched in Inspector its fine 

        // Look at this https://answers.unity.com/questions/1501743/scriptable-object-resets-randomly-between-scenes.html
        arrowSignal.Raise();
        coinSignal.Raise();
    }

    public void resetHealth()
    {
        HeartContainers.RuntimeValue = HeartContainers.initialValue;
        playerHealth.RuntimeValue = playerHealth.initialValue;
        heartSignal.Raise();
    }

    /*

        //######################################### Gear Safe & Load Testing ###########################################################################
        public void SafeGear()
        {
            if (playerInventory.currentWeapon)
            {
                FileStream file = File.Create("E:\\Secret_Save" + string.Format("/{0}.weapon", 1));
                BinaryFormatter binary = new BinaryFormatter();
                var json = JsonUtility.ToJson(playerInventory.currentWeapon);
                binary.Serialize(file, json);
                file.Close();
                Debug.Log(playerInventory.currentWeapon.itemName + "Saved!");
            }
        }

        public void LoadGear()
        {
            if (File.Exists("E:\\Secret_Save" + string.Format("/{0}.Weapon", 1)))
            {
                FileStream file = File.Open("E:\\Secret_Save" + string.Format("/{0}.weapon", 1), FileMode.Open);
                BinaryFormatter binary = new BinaryFormatter();
                JsonUtility.FromJsonOverwrite((string)binary.Deserialize(file), playerInventory.currentWeapon);
                file.Close();
            }
        }
    //#################################################################################################################################################
    */
    /*
        private void OnEnable()
        {

            LoadScriptables();
        }

        private void OnDisable()
        {
            SaveScriptables();
        }
    */
    //################################################################# Save EVERYTHING ###################################################################
    /*
       public void SaveScriptables()
       {
           for (int i = 0; i < chests.Count; i++)                                                                  //Safe ChestState to file
           {
               FileStream file = File.Create("E:\\Secret_Save" + string.Format("/{0}.chests", i));
              // FileStream file = File.Create(Application.persistentDataPath + string.Format("/{0}.chests", i));
               BinaryFormatter binary = new BinaryFormatter();
               var json = JsonUtility.ToJson(chests[i]);
               binary.Serialize(file, json);
               file.Close();
               Debug.Log(chests[i].name + "Saved!");
           }
           for (int i = 0; i < doors.Count; i++)                                                                   //Safe DoorState to file
           {
               FileStream file = File.Create("E:\\Secret_Save" + string.Format("/{0}.doors", i));
               // FileStream file = File.Create(Application.persistentDataPath + string.Format("/{0}.doors", i));
               BinaryFormatter binary = new BinaryFormatter();
               var json = JsonUtility.ToJson(doors[i]);
               binary.Serialize(file, json);
               file.Close();
               Debug.Log(doors[i].name + "Saved!");
           }
           for (int i = 0; i < playerInventory.myInventory.Count; i++)                                             //Safe MainInventory (no Equipment)
           {
               FileStream file = File.Create("E:\\Secret_Save" + string.Format("/{0}.inventory", i));
               // FileStream file = File.Create(Application.persistentDataPath + string.Format("/{0}.inventory", i));
               BinaryFormatter binary = new BinaryFormatter();
               var json = JsonUtility.ToJson(playerInventory.myInventory[i]);
               binary.Serialize(file, json);
               file.Close();
               Debug.Log(playerInventory.myInventory[i].name + "Saved!");
           }

       }

   */
    //################################################################# LOAD EVERYTHING ###################################################################
    /*
        public void LoadScriptables()
        {

            for (int i = 0; i < chests.Count; i++)
            {
                if (File.Exists("E:\\Secret_Save" + string.Format("/{0}.chests", i)))
                {
                    FileStream file = File.Open("E:\\Secret_Save" + string.Format("/{0}.chests", i), FileMode.Open);
                    BinaryFormatter binary = new BinaryFormatter();
                    JsonUtility.FromJsonOverwrite((string)binary.Deserialize(file), chests[i]);
                    file.Close();
                }
            }
            for (int i = 0; i < doors.Count; i++)
            {
                if (File.Exists("E:\\Secret_Save" + string.Format("/{0}.doors", i)))
                {
                    FileStream file = File.Open("E:\\Secret_Save" + string.Format("/{0}.doors", i), FileMode.Open);
                    BinaryFormatter binary = new BinaryFormatter();
                    JsonUtility.FromJsonOverwrite((string)binary.Deserialize(file), doors[i]);
                    file.Close();
                }
            }
            for (int i = 0; i < playerInventory.myInventory.Count; i++)
            {
                if (File.Exists("E:\\Secret_Save" + string.Format("/{0}.inventory", i)))
                {
                    FileStream file = File.Open("E:\\Secret_Save" + string.Format("/{0}.inventory", i), FileMode.Open);
                    BinaryFormatter binary = new BinaryFormatter();
                    JsonUtility.FromJsonOverwrite((string)binary.Deserialize(file), playerInventory.myInventory[i]);
                    file.Close();
                }
            }
            Debug.Log("Loaded!");
        }

    */
}
