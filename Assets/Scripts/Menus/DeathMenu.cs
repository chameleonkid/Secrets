﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    public void NewGame()
    {
        Debug.Log("Scene Laden!");
        SceneManager.LoadScene("StartMenu");
    }

    public void ExitGame() => Application.Quit();

    public void Load() => SimpleSave.Instance.Load("saveSlot1");
}
