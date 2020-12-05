using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{

    private bool isPaused;
    public GameObject pausePanel;
    public GameObject myEventSystem;
    public GameObject firstButtonPause;
    public GameObject InventoryActive;


    void Start()
    {
       isPaused = false;
       myEventSystem = GameObject.Find("EventSystem");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Pause") && !InventoryActive.activeInHierarchy)
        {
            ChangePause();
            if (firstButtonPause)
            {
                myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
                myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(firstButtonPause);
            }
           
        }
        
    }

    public void ChangePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }

    }


    public void Quit()
    {
    
    SceneManager.LoadScene("StartMenu");
    Time.timeScale = 1f;
    }



}
