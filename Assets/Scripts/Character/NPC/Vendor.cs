using UnityEngine;
using UnityEngine.EventSystems;

public class Vendor : MonoBehaviour
{
    public bool playerInRange;
    public Signals contextOn;
    public Signals contextOff;

    public Inventory vendorInventory;

    // private void Update()
    // {
    //     if (Input.GetButtonDown("Interact") && playerInRange && CanvasManager.Instance.IsFreeOrActive(this.gameObject))
    //     {
    //         setPause();
    //         if (firstButtonInventory)
    //         {
    //             EventSystem.current.SetSelectedGameObject(null);
    //             EventSystem.current.SetSelectedGameObject(firstButtonInventory);
    //         }
    //     }
    //     if (Input.GetButtonDown("Inventory") && vendorActive.activeInHierarchy)
    //     {
    //         stopPause();
    //     }
    // }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.CompareTag("Player") && other.isTrigger)
    //     {
    //         contextOn.Raise();
    //         playerInRange = true;
    //     }
    // }

    // private void OnTriggerExit2D(Collider2D other)
    // {
    //     if (other.CompareTag("Player") && other.isTrigger)
    //     {
    //         contextOff.Raise();
    //         playerInRange = false;
    //     }
    // }

    // public void setPause()
    // {
    //     vendorPanel.SetActive(true);
    //     Time.timeScale = 0f;
    // }
    // public void stopPause()
    // {
    //     inventoryPanel.SetActive(false);
    //     vendorPanel.SetActive(false);
    //     Time.timeScale = 1f;
    // }
}
