using UnityEngine;

public class Coin : PickUp
{
    [SerializeField] private Signals coinSignal = default;
    public Inventory PlayerInventory;

    public void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerMovement>();
        if (player != null)
        {
            PlayerInventory.coins += 1;
            coinSignal?.Raise();
            Destroy(this.gameObject);
        }
    }
}
