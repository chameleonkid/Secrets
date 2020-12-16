using UnityEngine;
using UnityEngine.EventSystems;

public class Vendor : InventoryScreenManager
{
    public bool playerInRange;
    public Signals contextOn;
    public Signals contextOff;

    public Inventory vendorInventory;
    public InventoryAmulet testAmu;

    private void Awake() => vendorInventory.Add(testAmu);

    private void Update()
    {
        if (Input.GetButtonDown("Interact") && playerInRange)
        {
            setPause();
            if (firstButtonInventory)
            {
                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(firstButtonInventory);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.isTrigger)
        {
            contextOn.Raise();
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.isTrigger)
        {
            contextOff.Raise();
            playerInRange = false;
        }
    }

    public void setPause()
    {
        inventoryPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void stopPause()
    {
        inventoryPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
