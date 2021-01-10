using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ScriptableObjectPersistence))]
public class SimpleSave : DDOLSingleton<SimpleSave>
{
    private ScriptableObjectPersistence so;

    private void Start() => so = GetComponent<ScriptableObjectPersistence>();

    public void Save()
    {
        SavePlayer();
        SaveInventory();
        SaveBools();

        ES3.Save("Scene", SceneManager.GetActiveScene().name);
        Debug.Log("Save completed");
    }

    public void Load()
    {

        LoadPlayer();
        LoadInventory();
        LoadBools();

        LoadScene(ES3.Load<string>("Scene"));
        Debug.Log("Loading completed");
    }

    public void LoadNew()
    {
        so.ResetScriptableObjects();

        LoadScene(SceneManager.GetActiveScene().name);  //! For development only, forces a scene reload on resetting.
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);

        // PauseMenü schließen und Zeit weiterlaufen lassen
        // pausePanel.SetActive(false);    //! Is this necessary if we are already loading the scene afresh?
        Time.timeScale = 1f;
    }

    private void SavePlayer()
    {
        so.playerPosition.value = FindObjectOfType<PlayerMovement>().transform.position;
        ES3.Save("Position", so.playerPosition);
        ES3.Save("Health", so.health);
        ES3.Save("Mana", so.mana);
        ES3.Save("Lumen", so.lumen);
    }

    private void SaveInventory()
    {
        ES3.Save("Inventory", so.playerInventory);
        ES3.Save("VendorInventory", so.vendorInventories);
        ES3.Save("Items", so.playerInventory.items.Serialize());

        //Safe Vendor-Items of ALL Vendors
        /*
        foreach (var item in so.vendorInventories)
        {
            ES3.Save("Items", so.vendorInventories.items.Serialize());
        }
        */
    }

    private void SaveBools()
    {
        ES3.Save("Chests", so.chests);
        ES3.Save("Doors", so.doors);
        ES3.Save("Bosses", so.bosses);
    }

    private void LoadPlayer()
    {
        ES3.Load("Position", so.playerPosition);
        ES3.Load("Health", so.health);
        ES3.Load("Mana", so.mana);
        ES3.Load("Lumen", so.lumen);
    }

    private void LoadInventory()
    {
        ES3.Load("Inventory", so.playerInventory);
        ES3.Load("VendorInventory", so.vendorInventories);


        so.playerInventory.items = (ES3.Load("Items") as Schwer.ItemSystem.SerializableInventory).Deserialize(so.itemDatabase);

        //Load all items from all vendors
        /*
        foreach (var item in so.vendorInventories)
        {
            so.vendorInventories.items = (ES3.Load("Items") as Schwer.ItemSystem.SerializableInventory).Deserialize(so.itemDatabase);
        }
        */
    }

    private void LoadBools()
    {
        ES3.Load("Chests", so.chests);
        ES3.Load("Doors", so.doors);
        ES3.Load("Bosses", so.bosses);
    }
}
