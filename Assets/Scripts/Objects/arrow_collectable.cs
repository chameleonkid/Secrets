using UnityEngine;

public class arrow_collectable : PickUp
{
    [SerializeField] private Signals arrowSignal = default;
    public PlayerInventory PlayerInventory;
    public InventoryItem arrow;

    public void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerMovement>();
        if (player != null)
        {
            PlayerInventory.Add(arrow);
            arrowSignal?.Raise();
            Destroy(this.gameObject);
        }
    }
}
