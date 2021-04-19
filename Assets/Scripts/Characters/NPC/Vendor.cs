using UnityEngine;

public class Vendor : MonoBehaviour
{
    [SerializeField] private Signals contextOn = default;
    [SerializeField] private Signals contextOff = default;

    [SerializeField] private Inventory vendorInventory = default;
    [SerializeField] private PlayerMovement player;

    private bool playerInRange;

    private void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        if (playerInRange && player.inputInteract)
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
