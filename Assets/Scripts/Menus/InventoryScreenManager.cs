using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventoryScreenManager : MonoBehaviour
{

    private bool isPaused;
    public GameObject inventoryPanel;
    public GameObject myEventSystem;
    public GameObject firstButtonInventory;
    public GameObject pauseActive;
    public GameObject vendorActive;

    // Start is called before the first frame update
    void Start()
    {
        isPaused = false;
        myEventSystem = GameObject.Find("EventSystem");

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory") && !pauseActive.activeInHierarchy && !vendorActive.activeInHierarchy)
        {
            ChangePause();
            if (firstButtonInventory)
            {
                myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
                myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(firstButtonInventory);
            }
        }

    }

    public void ChangePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            inventoryPanel.SetActive(true);
            Time.timeScale = 0f;

        }
        else
        {
            inventoryPanel.SetActive(false);
            Time.timeScale = 1f;
        }

    }


    public void Quit()
    {

        SceneManager.LoadScene("StartMenu");
        Time.timeScale = 1f;
    }





}
