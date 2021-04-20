using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject loadPanel;
    public GameObject firstButtonPause;
    [SerializeField] private PlayerMovement player;

    private bool isPaused = false;

    private void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        if(player)
        {
            if (player.inputLoad && CanvasManager.Instance.IsFreeOrActive(pausePanel))
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
    }

   
    private void ChangePause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;
        pausePanel.SetActive(isPaused);
        if (!isPaused)
        {
            CanvasManager.Instance.RegisterClosedCanvas(pausePanel);
        }
    }

    public void Quit()
    {
        SceneManager.LoadScene("StartMenu");
        Time.timeScale = 1f;
    }
         
    public void Save() => SaveManager.Instance.Save("saveSlot1");

    public void Reset() => SaveManager.Instance.LoadNew();
}
