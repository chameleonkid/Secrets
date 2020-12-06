using UnityEngine;

public class PhysicalInventoryItem : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public InventoryItem thisItem;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("powerup") && other.isTrigger)
        {
            if (playerInventory && thisItem)
            {
                playerInventory.Add(thisItem);
            }
            Destroy(this.gameObject);
        }
    }
}
