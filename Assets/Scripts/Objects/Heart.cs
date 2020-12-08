using UnityEngine;

public class Heart : PickUp
{
    [SerializeField] private float amountToIncrease = default;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.health.current += amountToIncrease;
            Destroy(this.gameObject);
        }
    }
}
