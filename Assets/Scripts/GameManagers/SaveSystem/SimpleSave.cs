using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(ScriptableObjectPersistence))]
public class SimpleSave : MonoBehaviour
{
    public static SimpleSave Instance { get; private set; }

    // public GameObject pausePanel;   // Refer to note in LoadScene

    private ScriptableObjectPersistence so;

    private void Awake() {
        if (Instance != null && Instance != this)
        {
            transform.parent = null;
            Destroy(this.gameObject);
        }
        else {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            so = GetComponent<ScriptableObjectPersistence>();
        }
    }

    public void Save()
    {
        SavePlayer();
        SaveInventory();
        SaveBools();

        ES3.Save("Scene", SceneManager.GetActiveScene().name);
    }

    public void Load()
    {
        LoadPlayer();
        LoadInventory();
        LoadBools();

        LoadScene(ES3.Load<string>("Scene"));
    }

    public void LoadNew()
    {
        so.ResetScriptableObjects();

        LoadScene(SceneManager.GetActiveScene().name);  //! For development only, forces a scene reload on resetting.
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);

        so.coinSignal.Raise();          //! Is this necessary if we are already loading the scene afresh?

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
    }

    private void SaveInventory()
    {
        ES3.Save("Inventory", so.playerInventory);
        ES3.Save("Items", so.playerInventory.items.Serialize());
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
    }

    private void LoadInventory()
    {
        ES3.Load("Inventory", so.playerInventory);
        so.playerInventory.items = (ES3.Load("Items") as Schwer.ItemSystem.SerializableInventory).Deserialize(so.itemDatabase);
    }

    private void LoadBools()
    {
        ES3.Load("Chests", so.chests);
        ES3.Load("Doors", so.doors);
        ES3.Load("Bosses", so.bosses);
    }
}
