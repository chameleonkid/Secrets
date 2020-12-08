using UnityEngine;

public class arrow_collectable : PowerUps
{
    [SerializeField] private Signals arrowSignal = default;
    public PlayerInventory PlayerInventory;
    public InventoryItem arrow;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("powerup") && other.isTrigger)
        {
            PlayerInventory.Add(arrow);
            arrowSignal?.Raise();
            Destroy(this.gameObject);
        }
    }
}
