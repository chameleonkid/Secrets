using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject firstButtonPause;
    public GameObject InventoryActive;
   // public GameObject vendorActive;

    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetButtonDown("Pause") && !InventoryActive.activeInHierarchy)
        {
            ChangePause();
            if (firstButtonPause)
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(firstButtonPause);
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
