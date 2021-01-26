using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SavePoint : MonoBehaviour
{
    private bool playerInRange;
    [SerializeField] private bool canOpen = true;
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
        if (playerInRange == true && Input.GetButtonDown("Interact") && canOpen)
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

    public void CancelSave()
    {
        canOpen = false;
        saveMenu.SetActive(false);
        Time.timeScale = 1;
        StartCoroutine(CallCloseCo());
    }

    public void Save(string saveSlot, Text saveSlotDisplay)
    {
        canOpen = false;
        saveName.RuntimeValue = playerAppearance.playerName + "\nLevel: " + playerXP.level + "\n" + SceneManager.GetActiveScene().name;

        SimpleSave.Instance.Save(saveSlot);

        saveSlotDisplay.text = saveName.RuntimeValue;

        Time.timeScale = 1;
        saveMenu.SetActive(false);
        Debug.Log("Game was saved!");
        StartCoroutine(CallCloseCo());
    }

    IEnumerator CallCloseCo()
    {
        yield return new WaitForSeconds(0.5f);
        canOpen = true;
    }
}
