using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
            loadPanel.SetActive(false);
            ChangePause();
            if (firstButtonPause)
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(firstButtonPause);
            }
        }
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
         
    public void Save() => SimpleSave.Instance.Save("saveSlot1");

    public void Reset() => SimpleSave.Instance.LoadNew();
}
