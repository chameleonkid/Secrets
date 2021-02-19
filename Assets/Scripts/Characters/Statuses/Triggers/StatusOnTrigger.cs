using UnityEngine;

public abstract class StatusOnTrigger<T> : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var target = other.GetComponent<T>();
        if (target != null)
        {
            Trigger(target);
        }
    }

    protected abstract void Trigger(T target);
}
