using UnityEngine;

public class Coin : PowerUps
{
    [SerializeField] private Signals coinSignal = default;
    public PlayerInventory PlayerInventory;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("powerup") && other.isTrigger)
        {
            PlayerInventory.coins += 1;
            coinSignal?.Raise();
            Destroy(this.gameObject);
        }
    }
}
