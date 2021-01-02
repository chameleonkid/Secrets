using UnityEngine;

public abstract class PickUp : MonoBehaviour
{
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerMovement>();
        if (player != null)
        {
            PlayerPickUp(player);
        }
    }

    protected abstract void PlayerPickUp(PlayerMovement player);
}
