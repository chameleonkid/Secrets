using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class InventoryScreenManager : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject firstButtonInventory;

    private bool isPaused = false;

    private void Update()
    {
        if (Input.GetButtonDown("Inventory") && CanvasManager.Instance.IsFreeOrActive(inventoryPanel.gameObject))
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
        Time.timeScale = isPaused ? 0 : 1;
        inventoryPanel.SetActive(isPaused);
    }

    public void Quit()
    {
        SceneManager.LoadScene("StartMenu");
        Time.timeScale = 1f;
    }
}
