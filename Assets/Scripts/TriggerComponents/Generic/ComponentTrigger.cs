using UnityEngine;

public abstract class ComponentTrigger<T> : MonoBehaviour where T : MonoBehaviour
{
    protected abstract bool? needOtherIsTrigger { get; }

    private bool passTriggerCheck => !(needOtherIsTrigger.HasValue && !needOtherIsTrigger.Value);

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (passTriggerCheck)
        {
            var component = other.GetComponent<T>();
            if (component != null)
            {
                OnEnter(component);
            }
        }
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        if (passTriggerCheck)
        {
            var component = other.GetComponent<T>();
            if (component != null)
            {
                OnExit(component);
            }
        }
    }

    protected virtual void OnEnter(T component) {}
    protected virtual void OnExit(T component) {}
}
