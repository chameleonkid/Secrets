using Schwer;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ScriptableObjectPersistence))]
public class SimpleSave : DDOLSingleton<SimpleSave>
{
    private ScriptableObjectPersistence so;

    private void Start() => so = GetComponent<ScriptableObjectPersistence>();

    public void Save(string saveSlot)
    {
        ES3.Save("Name", so.saveName.RuntimeValue, saveSlot);

        SavePlayer(saveSlot);
        SaveVendorInventories(saveSlot);
        SaveBools(saveSlot);
        SaveAppearance(saveSlot);
        SaveTime(saveSlot);

        ES3.Save("Scene", SceneManager.GetActiveScene().name, saveSlot);
        Debug.Log("Save completed");
    }

    public void Load(string loadSlot)
    {
        Debug.Log("Trying to load " + loadSlot);

        so.saveName.RuntimeValue = ES3.Load("Name", loadSlot, so.saveName.initialValue);

        LoadPlayer(loadSlot);
        LoadVendorInventories(loadSlot);
        LoadBools(loadSlot);
        LoadAppearance(loadSlot);
        LoadTime(loadSlot);

        LoadScene(ES3.Load<string>("Scene", loadSlot));
        Debug.Log("Loading completed");
    }

    public void LoadNew()
    {
        so.ResetScriptableObjects();

        if (!(SceneManager.GetActiveScene().name == "StartMenu"))
        {
            LoadScene(SceneManager.GetActiveScene().name);  //! For development only, forces a scene reload on resetting.
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);

        // PauseMenü schließen und Zeit weiterlaufen lassen
        // pausePanel.SetActive(false);    //! Is this necessary if we are already loading the scene afresh?
        Time.timeScale = 1f;
    }

    private void SaveAppearance(string saveSlot) => ES3.Save("Appearance", so.characterAppearance.GetSerializable(), saveSlot);

    private void LoadAppearance(string loadSlot)
    {
        var cas = (CharacterAppearance.CharacterAppearanceSerializable)ES3.Load("Appearance", loadSlot);
        so.characterAppearance.Deserialize(cas, so.skinTexturesDatabase);
    }

    private void SavePlayer(string saveSlot)
    {
        so.playerPosition.value = FindObjectOfType<PlayerMovement>().transform.position;
        ES3.Save("Position", so.playerPosition, saveSlot);
        ES3.Save("Health", so.health, saveSlot);
        ES3.Save("Mana", so.mana, saveSlot);
        ES3.Save("Lumen", so.lumen, saveSlot);
        ES3.Save("XP", so.xpSystem, saveSlot);

        SaveInventory("Inventory", so.playerInventory, saveSlot);
    }

    private void LoadPlayer(string loadSlot)
    {
        ES3.LoadInto("Position", loadSlot, so.playerPosition);
        ES3.LoadInto("Health", loadSlot, so.health);
        ES3.LoadInto("Mana", loadSlot, so.mana);
        ES3.LoadInto("Lumen", loadSlot, so.lumen);
        ES3.LoadInto("XP", loadSlot, so.xpSystem);

        LoadInventory("Inventory", loadSlot, so.playerInventory, so.itemDatabase);
    }

    private void SaveInventory(string key, Inventory inventory, string filePath)
    {
        var serializableInventory = new Inventory.SerializableInventory(inventory);
        ES3.Save(key, serializableInventory, filePath);
    }

    private void LoadInventory(string key, string filePath, Inventory inventory, Schwer.ItemSystem.ItemDatabase itemDatabase)
    {
        var serializableInventory = ES3.Load(key, filePath) as Inventory.SerializableInventory;
        serializableInventory.DeserializeInto(inventory, itemDatabase);
    }

    private void SaveVendorInventories(string saveSlot)
    {
        //! TODO
        ES3.Save("VendorInventory", so.vendorInventories, saveSlot);
    }

    private void LoadVendorInventories(string loadSlot)
    {
        //! TODO
        ES3.LoadInto("VendorInventory", loadSlot, so.vendorInventories);
    }

    private void SaveBools(string saveSlot)
    {
        ES3.Save("Chests", so.chests, saveSlot);
        ES3.Save("Doors", so.doors, saveSlot);
        ES3.Save("Bosses", so.bosses, saveSlot);
        ES3.Save("HealthCrystals", so.healthCrystals, saveSlot);
        ES3.Save("ManaCrystals", so.manaCrystals, saveSlot);
    }

    private void LoadBools(string loadSlot)
    {
        ES3.LoadInto("Chests", loadSlot, so.chests);
        ES3.LoadInto("Doors", loadSlot, so.doors);
        ES3.LoadInto("Bosses", loadSlot, so.bosses);
        ES3.LoadInto("HealthCrystals", loadSlot, so.healthCrystals);
        ES3.LoadInto("ManaCrystals", loadSlot, so.manaCrystals);
    }

    private void SaveTime(string saveSlot) => ES3.Save("Time", so.timeOfDay, saveSlot);

    private void LoadTime(string loadSlot) => ES3.LoadInto("Time", loadSlot, so.timeOfDay);
}
