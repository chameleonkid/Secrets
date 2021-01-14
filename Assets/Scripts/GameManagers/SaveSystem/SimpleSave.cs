using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ScriptableObjectPersistence))]
public class SimpleSave : DDOLSingleton<SimpleSave>
{
    private ScriptableObjectPersistence so;

    private void Start() => so = GetComponent<ScriptableObjectPersistence>();

    public void Save(string saveSlot)
    {
        SavePlayer(saveSlot);
        SaveInventory(saveSlot);
        SaveBools(saveSlot);
        SaveAppearance(saveSlot);

        ES3.Save("Scene", SceneManager.GetActiveScene().name, saveSlot);
        Debug.Log("Save completed");
    }

    public void Load(string loadSlot)
    {
        Debug.Log("Trying to load " + loadSlot);
        LoadPlayer(loadSlot);
        LoadInventory(loadSlot);
        LoadBools(loadSlot);
        LoadAppearance(loadSlot);

        LoadScene(ES3.Load<string>("Scene",loadSlot));            // This needs to be asked... I NEED SCHWER!
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
    private void SaveAppearance(string saveSlot)
    {
        ES3.Save("Appearance", so.characterAppearance, saveSlot);
    }

    private void LoadAppearance(string loadSlot)
    {
        ES3.Load("Appearance", loadSlot, so.characterAppearance);
    }

    private void SavePlayer(string saveSlot)
    {
        so.playerPosition.value = FindObjectOfType<PlayerMovement>().transform.position;
        ES3.Save("Position", so.playerPosition, saveSlot);
        ES3.Save("Health", so.health, saveSlot);
        ES3.Save("Mana", so.mana, saveSlot);
        ES3.Save("Lumen", so.lumen, saveSlot);
    }

    private void LoadPlayer(string loadSlot)
    {
        ES3.Load("Position", loadSlot, so.playerPosition);
        ES3.Load("Health", loadSlot, so.health);
        ES3.Load("Mana", loadSlot, so.mana);
        ES3.Load("Lumen", loadSlot, so.lumen);
    }

    private void SaveInventory(string saveSlot)
    {
        ES3.Save("Inventory", so.playerInventory, saveSlot);
        ES3.Save("VendorInventory", so.vendorInventories, saveSlot);
        ES3.Save("Items", so.playerInventory.items.Serialize(), saveSlot);
    }

    private void LoadInventory(string loadSlot)
    {
        ES3.Load("Inventory", loadSlot, so.playerInventory);
        ES3.Load("VendorInventory", loadSlot, so.vendorInventories);

        so.playerInventory.items = (ES3.Load("Items", loadSlot) as Schwer.ItemSystem.SerializableInventory).Deserialize(so.itemDatabase);
    }

    private void SaveBools(string saveSlot)
    {
        ES3.Save("Chests", so.chests, saveSlot);
        ES3.Save("Doors", so.doors, saveSlot);
        ES3.Save("Bosses", so.bosses, saveSlot);
        ES3.Save("HealthCrystals", so.healthCrystals, saveSlot);
        ES3.Save("ManaCrystals", so.manaCrystals, saveSlot);
        ES3.Save("SaveSlotNames", so.saveSlotNames, saveSlot);
    }

    private void LoadBools(string loadSlot)
    {
        ES3.Load("Chests", loadSlot, so.chests);
        ES3.Load("Doors", loadSlot, so.doors);
        ES3.Load("Bosses", loadSlot, so.bosses);
        ES3.Load("HealthCrystals", loadSlot, so.healthCrystals);
        ES3.Load("ManaCrystals", loadSlot, so.manaCrystals);
        ES3.Load("SaveSlotNames", loadSlot, so.saveSlotNames);
    }
}
