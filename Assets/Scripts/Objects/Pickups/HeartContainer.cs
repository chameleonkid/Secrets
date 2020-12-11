using UnityEngine;

public class HeartContainer : PickUp
{
    [SerializeField] private float amountToIncrease = default;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.healthMeter.max += amountToIncrease;
            player.health = player.healthMeter.max;
            Destroy(this.gameObject);
        }
    }
}
