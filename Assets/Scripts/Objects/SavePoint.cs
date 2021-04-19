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
    [SerializeField] private PlayerMovement player;


    private void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMovement>();
    }

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
        if (playerInRange == true && player.inputInteract && CanvasManager.Instance.IsFreeOrActive(saveMenu) && Time.timeScale > 0)
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

    public void SaveSlot1() => Save(SaveUtility.SaveSlots[0], saveSlot1);

    public void SaveSlot2() => Save(SaveUtility.SaveSlots[1], saveSlot2);

    public void SaveSlot3() => Save(SaveUtility.SaveSlots[2], saveSlot3);

    public void CancelSave() => CloseMenu();

    public void Save(string saveSlot, Text saveSlotDisplay)
    {
        saveName.RuntimeValue = playerAppearance.playerName + "\nLevel: " + playerXP.level + "\n" + SceneManager.GetActiveScene().name;

        SaveManager.Instance.Save(saveSlot);

        saveSlotDisplay.text = saveName.RuntimeValue;

        Debug.Log("Game was saved!");
        
        CloseMenu();
    }

    private void CloseMenu()
    {
        saveMenu.SetActive(false);
        Time.timeScale = 1;
        CanvasManager.Instance.RegisterClosedCanvas(saveMenu);
    }
}
