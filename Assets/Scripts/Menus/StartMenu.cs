using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject loadPanel;
    public GameObject firstButtonPause;

    [SerializeField] private Button saveSlot1;
    [SerializeField] private Button saveSlot2;
    [SerializeField] private Button saveSlot3;
    [SerializeField] private StringValue saveSlot1Text;
    [SerializeField] private StringValue saveSlot2Text;
    [SerializeField] private StringValue saveSlot3Text;


    public void NewGame()
    {
        SceneManager.LoadScene("CharacterCreation");
    }

    public void ClickLoadButton()
    {
        loadPanel.SetActive(true);
        saveSlot1.GetComponentInChildren<Text>().text = saveSlot1Text.RuntimeValue;
        saveSlot2.GetComponentInChildren<Text>().text = saveSlot2Text.RuntimeValue;
        saveSlot3.GetComponentInChildren<Text>().text = saveSlot3Text.RuntimeValue;
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
