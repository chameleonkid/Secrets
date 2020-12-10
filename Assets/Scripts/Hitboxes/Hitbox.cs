using UnityEngine;

public abstract class Hitbox : MonoBehaviour
{
    [Tooltip("The tag this hitbox is able to affect")]
    [SerializeField] private string targetTag = default;

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger && other.CompareTag(targetTag))
        {
            OnHit(other);
        }
    }

    protected abstract void OnHit(Collider2D other);
}
