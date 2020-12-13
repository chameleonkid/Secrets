using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject firstButtonPause;
    public GameObject InventoryActive;

    private EventSystem eventSystem;
    private bool isPaused = false;

    private void Awake() => eventSystem = GetComponentInChildren<EventSystem>();

    private void Update()
    {
        if (Input.GetButtonDown("Pause") && !InventoryActive.activeInHierarchy)
        {
            ChangePause();
            if (firstButtonPause)
            {
                eventSystem.SetSelectedGameObject(null);
                eventSystem.SetSelectedGameObject(firstButtonPause);
            }
        }
    }

    public void ChangePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void Quit()
    {
        SceneManager.LoadScene("StartMenu");
        Time.timeScale = 1f;
    }

    public void Save() => SimpleSave.Instance.Save();
    public void Load() => SimpleSave.Instance.Load();
    public void Reset() => SimpleSave.Instance.LoadNew();
}
