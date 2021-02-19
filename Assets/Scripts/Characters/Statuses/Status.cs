using UnityEngine;

public abstract class Status : MonoBehaviour
{
    public abstract void Trigger();
    public abstract void Clear();

    protected abstract void Update();
}
