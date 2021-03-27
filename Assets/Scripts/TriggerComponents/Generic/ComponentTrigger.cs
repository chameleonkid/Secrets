using UnityEngine;

public abstract class ComponentTrigger<T> : MonoBehaviour where T : MonoBehaviour
{
    protected void OnTriggerEnter2D(Collider2D other)
    {
        var component = other.GetComponent<T>();
        if (component != null)
        {
            OnTriggerEnter2D(component);
        }
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        var component = other.GetComponent<T>();
        if (component != null)
        {
            OnTriggerExit2D(component);
        }
    }

    protected virtual void OnTriggerEnter2D(T component) {}
    protected virtual void OnTriggerExit2D(T component) {}
}
