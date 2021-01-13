using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject loadPanel;
    public GameObject firstButtonPause;

    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetButtonDown("Pause") && CanvasManager.Instance.IsFreeOrActive(pausePanel.gameObject))
        {
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


    public void LoadSlot1()
    {
        Load("saveSlot1");
    }

    public void LoadSlot2()
    {
        Load("saveSlot2");
    }

    public void LoadSlot3()
    {
        Load("saveSlot3");
    }

    public void Load(string loadSlot)
    {
        SimpleSave.Instance.Load(loadSlot);
        loadPanel.SetActive(false);
    }

    public void CancelLoad()
    {
        loadPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButtonPause);
    }


    public void Save() => SimpleSave.Instance.Save("SecretsSave_DEV");

    public void Reset() => SimpleSave.Instance.LoadNew();
}
