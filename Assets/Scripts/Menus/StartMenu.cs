using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public GameObject pausePanel;

    public void NewGame()
    {
        SceneManager.LoadScene("CharacterCreation");
    }


    public void ExitGame()
    {
        Application.Quit();
    }

}
