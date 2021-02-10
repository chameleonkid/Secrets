using Schwer;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ScriptableObjectPersistence))]
public class SaveManager : DDOLSingleton<SaveManager>
{
    private ScriptableObjectPersistence _so;
    public ScriptableObjectPersistence so => _so;

    private void Start() => _so = GetComponent<ScriptableObjectPersistence>();

    public void Save(string saveSlot)
    {
        ES3.Save("Name", _so.saveName.RuntimeValue, saveSlot);

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

        _so.saveName.RuntimeValue = ES3.Load("Name", loadSlot, _so.saveName.initialValue);

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
        _so.ResetScriptableObjects();

        if (!(SceneManager.GetActiveScene().name == "StartMenu"))
        {
            LoadScene(SceneManager.GetActiveScene().name);  //! For development only, forces a scene reload on resetting.
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        // pausePanel.SetActive(false);    //! Is this necessary if we are already loading the scene afresh?
        Time.timeScale = 1f;
    }

    private void SaveAppearance(string saveSlot) => ES3.Save("Appearance", _so.characterAppearance.GetSerializable(), saveSlot);

    private void LoadAppearance(string loadSlot)
    {
        var cas = (CharacterAppearance.CharacterAppearanceSerializable)ES3.Load("Appearance", loadSlot);
        _so.activeTexturesDatabase = _so.characterAppearance.Deserialize(cas, _so.skinTexturesDatabases);
    }

    private void SavePlayer(string saveSlot)
    {
        _so.playerPosition.value = FindObjectOfType<PlayerMovement>().transform.position;
        ES3.Save("Position", _so.playerPosition, saveSlot);
        ES3.Save("Health", _so.health, saveSlot);
        ES3.Save("Mana", _so.mana, saveSlot);
        ES3.Save("Lumen", _so.lumen, saveSlot);
        ES3.Save("XP", _so.xpSystem, saveSlot);

        SaveInventory("Inventory", _so.playerInventory, saveSlot);
    }

    private void LoadPlayer(string loadSlot)
    {
        ES3.LoadInto("Position", loadSlot, _so.playerPosition);
        ES3.LoadInto("Health", loadSlot, _so.health);
        ES3.LoadInto("Mana", loadSlot, _so.mana);
        ES3.LoadInto("Lumen", loadSlot, _so.lumen);
        ES3.LoadInto("XP", loadSlot, _so.xpSystem);

        LoadInventory("Inventory", loadSlot, _so.playerInventory, _so.itemDatabase);
    }

    private void SaveInventory(string key, Inventory inventory, string filePath)
    {
        var serializableInventory = new Inventory.SerializableInventory(inventory);
        ES3.Save(key, serializableInventory, filePath);
    }

    private void LoadInventory(string key, string filePath, Inventory inventory, Schwer.ItemSystem.ItemDatabase itemDatabase)
    {
        var serializableInventory = ES3.Load(key, filePath) as Inventory.SerializableInventory;
        serializableInventory?.DeserializeInto(inventory, itemDatabase);
    }

    private void SaveVendorInventories(string saveSlot)
    {
        for (int i = 0; i < _so.vendorInventories.Length; i++)
        {
            SaveInventory("VendorInventory" + _so.vendorInventories[i].regular.name, _so.vendorInventories[i].regular, saveSlot);
        }
    }

    private void LoadVendorInventories(string loadSlot)
    {
        for (int i = 0; i < _so.vendorInventories.Length; i++)
        {
            LoadInventory("VendorInventory"  + _so.vendorInventories[i].regular.name , loadSlot, _so.vendorInventories[i].regular, _so.itemDatabase);
        }
    }

    private void SaveBools(string saveSlot)
    {
        ES3.Save("Chests", _so.chests, saveSlot);
        ES3.Save("Doors", _so.doors, saveSlot);
        ES3.Save("Bosses", _so.bosses, saveSlot);
        ES3.Save("HealthCrystals", _so.healthCrystals, saveSlot);
        ES3.Save("ManaCrystals", _so.manaCrystals, saveSlot);
    }

    private void LoadBools(string loadSlot)
    {
        ES3.LoadInto("Chests", loadSlot, _so.chests);
        ES3.LoadInto("Doors", loadSlot, _so.doors);
        ES3.LoadInto("Bosses", loadSlot, _so.bosses);
        ES3.LoadInto("HealthCrystals", loadSlot, _so.healthCrystals);
        ES3.LoadInto("ManaCrystals", loadSlot, _so.manaCrystals);
    }

    private void SaveTime(string saveSlot) => ES3.Save("Time", _so.timeOfDay, saveSlot);

    private void LoadTime(string loadSlot) => ES3.LoadInto("Time", loadSlot, _so.timeOfDay);
}
