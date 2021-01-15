using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SavePoint : MonoBehaviour
{
    private bool playerInRange;
    [SerializeField] private GameObject saveMenu;
    public GameObject firstButtonSave;
    [SerializeField] private StringValue saveName = default;

    [Header("Slot Values")]
    [SerializeField] private Text saveSlot1;
    [SerializeField] private Text saveSlot2;
    [SerializeField] private Text saveSlot3;

    [Header("Player Data")]
    [SerializeField] CharacterAppearance playerAppearance;
    [SerializeField] XPSystem playerXP;

    private void Awake()
    {
        saveMenu.SetActive(false);

        var saveNames = SaveUtility.GetSaveNames();
        saveSlot1.text = saveNames[0];
        saveSlot2.text = saveNames[1];
        saveSlot3.text = saveNames[2];
    }

    private void Update()
    {
        if (playerInRange == true && Input.GetButtonDown("Interact"))
        {
            saveMenu.SetActive(true);
            Time.timeScale = 0;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstButtonSave);
            // Debug.Log("Game was saved!");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.isTrigger)
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.isTrigger)
        {
            playerInRange = false;
        }
    }

    public void SaveSlot1()
    {
        saveName.RuntimeValue = playerAppearance.playerName + "\nLevel: " + playerXP.level + "\n" + SceneManager.GetActiveScene().name;
        Save(SaveUtility.SaveSlots[0]);
        saveSlot1.text = saveName.RuntimeValue;
    }

    public void SaveSlot2()
    {
        saveName.RuntimeValue = playerAppearance.playerName + "\nLevel: " + playerXP.level + "\n" + SceneManager.GetActiveScene().name;
        Save(SaveUtility.SaveSlots[1]);
        saveSlot2.text = saveName.RuntimeValue;
    }

    public void SaveSlot3()
    {
        saveName.RuntimeValue = playerAppearance.playerName + "\nLevel: " + playerXP.level + "\n" + SceneManager.GetActiveScene().name;
        Save(SaveUtility.SaveSlots[2]);
        saveSlot3.text = saveName.RuntimeValue;
    }

    public void CancelSave()
    {
        saveMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void Save(string saveSlot)
    {
        SimpleSave.Instance.Save(saveSlot);
        Time.timeScale = 1;
        saveMenu.SetActive(false);
        Debug.Log("Game was saved!");
    }
}
