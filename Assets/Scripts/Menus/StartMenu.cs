using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene("CharacterCreation");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void Load() => SimpleSave.Instance.Load();
}
