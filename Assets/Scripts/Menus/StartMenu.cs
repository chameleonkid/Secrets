using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class StartMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject loadPanel;
    public GameObject firstButtonPause;


    public void NewGame()
    {
        SceneManager.LoadScene("CharacterCreation");
    }

    public void ClickLoadButton()
    {
        loadPanel.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
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

    public void CancelLoad()
    {
        loadPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButtonPause);
    }

    public void Load(string loadSlot)
    {
        SimpleSave.Instance.Load(loadSlot);
        loadPanel.SetActive(false);
    }
}
