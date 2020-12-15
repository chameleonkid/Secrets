using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Vendor : InventoryScreenManager
{
    public bool playerInRange;
    public Signals contextOn;
    public Signals contextOff;

    public Inventory vendorInventory;
    public InventoryAmulet testAmu;
    public InventoryArmor testArmor;
    public InventoryWeapon testWeapon;

  

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Interact") && playerInRange && !pauseActive.activeInHierarchy)
        {
            setPause();
            if (firstButtonInventory)
            {
                myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(null);
                myEventSystem.GetComponent<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(firstButtonInventory);
            }
        }
        if (Input.GetButtonDown("Inventory") && vendorActive.activeInHierarchy)
        {
            stopPause();
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
        vendorPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void stopPause()
    {
        inventoryPanel.SetActive(false);
        vendorPanel.SetActive(false);
        Time.timeScale = 1f;
    }


}
