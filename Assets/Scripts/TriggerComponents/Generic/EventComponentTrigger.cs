using System;
using UnityEngine;

public class EventComponentTrigger<T> : ComponentTrigger<T> where T : MonoBehaviour
{
    public event Action OnTriggerEnter;
    public event Action OnTriggerExit;

    protected override void OnTriggerEnter2D(T component) => OnTriggerEnter?.Invoke();
    protected override void OnTriggerExit2D(T component) => OnTriggerExit?.Invoke();
}
