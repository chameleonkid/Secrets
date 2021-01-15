using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject loadPanel;
    public GameObject firstButtonPause;

    [SerializeField] private Button saveSlot1;
    [SerializeField] private Button saveSlot2;
    [SerializeField] private Button saveSlot3;

    public void NewGame() => SceneManager.LoadScene("CharacterCreation");

    public void ClickLoadButton()
    {
        loadPanel.SetActive(true);

        var saveNames = SaveUtility.GetSaveNames();
        saveSlot1.GetComponentInChildren<Text>().text = saveNames[0];
        saveSlot2.GetComponentInChildren<Text>().text = saveNames[1];
        saveSlot3.GetComponentInChildren<Text>().text = saveNames[2];
    }

    public void ExitGame() => Application.Quit();

    public void LoadSlot1() => Load(SaveUtility.SaveSlots[0]);

    public void LoadSlot2() => Load(SaveUtility.SaveSlots[1]);

    public void LoadSlot3() => Load(SaveUtility.SaveSlots[2]);

    public void CancelLoad()
    {
        loadPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButtonPause);
    }

    public void Load(string loadSlot)
    {
        SimpleSave.Instance.Load(loadSlot);
        loadPanel.SetActive(false);
    }
}
