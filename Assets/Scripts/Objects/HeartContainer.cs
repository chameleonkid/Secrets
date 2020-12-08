using UnityEngine;

public class HeartContainer : PickUp
{
    [SerializeField] private float amountToIncrease = default;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.health.max += amountToIncrease;
            player.health.current = player.health.max;
            Destroy(this.gameObject);
        }
    }
}
