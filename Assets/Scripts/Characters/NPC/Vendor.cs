using UnityEngine;

public class Vendor : MonoBehaviour
{
    [SerializeField] private Signals contextOn = default;
    [SerializeField] private Signals contextOff = default;

    [SerializeField] private Inventory vendorInventory = default;

    private bool playerInRange;

    private void Update()
    {
        if (Input.GetButtonDown("Interact") && playerInRange)
        {
            VendorManager.RequestInterface(vendorInventory);
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
}
