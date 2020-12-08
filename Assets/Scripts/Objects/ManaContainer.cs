using UnityEngine;

public class ManaContainer : PickUp
{
    [SerializeField] private float amountToIncrease = default;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerMovement>();
        if (player != null)
        {
            player.mana.max += amountToIncrease;
            player.mana.current = player.mana.max;
            Destroy(this.gameObject);
        }
    }
}
