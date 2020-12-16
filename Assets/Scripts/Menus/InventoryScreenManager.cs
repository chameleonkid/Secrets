using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class InventoryScreenManager : MonoBehaviour
{
    private bool isPaused;
    public GameObject inventoryPanel;
    public GameObject vendorPanel;
    public GameObject firstButtonInventory;
    public GameObject pauseActive;
    public GameObject vendorActive;


    private void Start() => isPaused = false;

    private void Update()
    {
        if (Input.GetButtonDown("Inventory") && !pauseActive.activeInHierarchy)
        {
            ChangePause();
            if (firstButtonInventory)
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(firstButtonInventory);
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
