using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SavePoint : MonoBehaviour
{
    private bool playerInRange;
    [SerializeField] private GameObject saveMenu;
    public GameObject firstButtonSave;
    [Header("Slot Values")]
    [SerializeField] private Text saveSlot1;
    [SerializeField] private Text saveSlot2;
    [SerializeField] private Text saveSlot3;
    [SerializeField] private StringValue saveSlot1Text;
    [SerializeField] private StringValue saveSlot2Text;
    [SerializeField] private StringValue saveSlot3Text;

    [Header("Player Data")]
    [SerializeField] CharacterAppearance playerAppearance;
    [SerializeField] XPSystem playerXP;

    private void Awake()
    {
        saveMenu.SetActive(false);
        saveSlot1.text = saveSlot1Text.RuntimeValue;
        saveSlot2.text = saveSlot2Text.RuntimeValue;
        saveSlot3.text = saveSlot3Text.RuntimeValue;
    }

    private void Update()
    {
        if (playerInRange == true && Input.GetButtonDown("Interact"))     // Create new button Interact instead of run
        {
            saveMenu.SetActive(true);
            Time.timeScale = 0;
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(firstButtonSave);
            // Save("SecretsSave");                 // <--- Instead of directly save you can open your canvas
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
        Save("saveSlot1");
        saveSlot1.text = playerAppearance.playerName + " Level: " + playerXP.level + " " + SceneManager.GetActiveScene().name;
        saveSlot1Text.RuntimeValue = saveSlot1.text;
    }

    public void SaveSlot2()
    {
        Save("saveSlot2");
        saveSlot2.text = playerAppearance.playerName + " Level: " + playerXP.level + " " + SceneManager.GetActiveScene().name;
        saveSlot2Text.RuntimeValue = saveSlot2.text;
    }

    public void SaveSlot3()
    {
        Save("saveSlot3");
        saveSlot3.text = playerAppearance.playerName + " Level: " + playerXP.level + " " + SceneManager.GetActiveScene().name;
        saveSlot3Text.RuntimeValue = saveSlot3.text;
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
