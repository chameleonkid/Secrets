using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StartMenu : MonoBehaviour
{
    public GameObject pausePanel;
    [SerializeField] private Button[] mainButtons;
    [SerializeField] private GameObject slot1Button;



    public void NewGame() => SceneManager.LoadScene("CharacterCreation");

    public void ClickStart() => SaveManager.Instance.LoadNew();

    public void Quit() => Application.Quit();

    public void LoadClick()
    {
        for(int i = 0; i < mainButtons.Length; i++)
        {
            mainButtons[i].interactable = false;
        }

        GameObject myEventSystem = GameObject.Find("EventSystem");
        myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(slot1Button);
    }

    public void CancelLoadClick()
    {
        for (int i = 0; i < mainButtons.Length; i++)
        {
            mainButtons[i].interactable = true;
        }
    }
}
