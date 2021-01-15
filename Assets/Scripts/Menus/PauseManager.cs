using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject loadPanel;
    public GameObject firstButtonPause;
    [SerializeField] private Button saveSlot1;
    [SerializeField] private Button saveSlot2;
    [SerializeField] private Button saveSlot3;

    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetButtonDown("Pause") && CanvasManager.Instance.IsFreeOrActive(pausePanel.gameObject))
        {
            loadPanel.SetActive(false);
            ChangePause();
            if (firstButtonPause)
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(firstButtonPause);
            }
        }
    }

    public void ClickLoadButton()
    {
        loadPanel.SetActive(true);

        var saveNames = SaveUtility.GetSaveNames();
        saveSlot1.GetComponentInChildren<Text>().text = saveNames[0];
        saveSlot2.GetComponentInChildren<Text>().text = saveNames[1];
        saveSlot3.GetComponentInChildren<Text>().text = saveNames[2];
    }

    private void ChangePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        pausePanel.SetActive(isPaused);
    }

    public void Quit()
    {
        SceneManager.LoadScene("StartMenu");
        Time.timeScale = 1f;
    }

    public void LoadSlot1() => Load(SaveUtility.SaveSlots[0]);

    public void LoadSlot2() => Load(SaveUtility.SaveSlots[1]);

    public void LoadSlot3() => Load(SaveUtility.SaveSlots[2]);

    public void Load(string loadSlot)
    {
        SimpleSave.Instance.Load(loadSlot);
        loadPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void CancelLoad()
    {
        loadPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButtonPause);
    }

    public void Save() => SimpleSave.Instance.Save(SaveUtility.SaveSlots[0]);

    public void Reset() => SimpleSave.Instance.LoadNew();
}
