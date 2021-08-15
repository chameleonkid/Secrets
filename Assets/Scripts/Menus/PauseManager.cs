﻿using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject loadPanel;
    public GameObject firstButtonPause;
    [SerializeField] private BoolValue torchAndBrazierParticlesOn;


    private void Update()
    {
        if (CanvasManager.Instance.IsFreeOrActive(pausePanel))
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
        var isPaused = !pausePanel.activeSelf;
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

    public void SwapParticles()
    {
        torchAndBrazierParticlesOn.RuntimeValue = !torchAndBrazierParticlesOn.RuntimeValue;
    }
}
