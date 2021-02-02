using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public GameObject pausePanel;

    public void NewGame() => SceneManager.LoadScene("CharacterCreation");

    public void ClickStart() => SaveManager.Instance.LoadNew();

    public void Quit() => Application.Quit();
}
