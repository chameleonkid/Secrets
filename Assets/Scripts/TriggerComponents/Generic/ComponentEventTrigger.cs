﻿using System;
using UnityEngine;

public abstract class ComponentEventTrigger<T> : ComponentTrigger<T> where T : MonoBehaviour
{
    public event Action OnTriggerEnter;
    public event Action OnTriggerExit;

    protected override void OnEnter(T component) => OnTriggerEnter?.Invoke();
    protected override void OnExit(T component) => OnTriggerExit?.Invoke();
}
