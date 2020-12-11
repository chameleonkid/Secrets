using UnityEngine;

public abstract class Hitbox : MonoBehaviour
{
    [Tooltip("The tag this hitbox is able to affect")]
    [SerializeField] private string[] targetTag = default;

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger)
        {
            for (int i = 0; i < targetTag.Length; i++)
            {
                if (other.CompareTag(targetTag[i]))
                {
                    OnHit(other);
                    return;
                }
            }
        }
    }

    protected abstract void OnHit(Collider2D other);
}
