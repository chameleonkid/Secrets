using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public void NewGame()
    {
        Debug.Log("Scene Laden!");
        SceneManager.LoadScene("StartMenu");
    }

    public void ExitGame() => Application.Quit();
}
