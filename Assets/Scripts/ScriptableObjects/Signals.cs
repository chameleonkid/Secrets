using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]

public class Signals : ScriptableObject
{
    public List<SignalsListener> listeners = new List<SignalsListener>();
    public void Raise()
    {
        for(int i = listeners.Count - 1; i>= 0; i --)
        {
            listeners[i].OnSignalRaised();
        }
       
    }
    public void RegisterListener(SignalsListener listener)
    {
        listeners.Add(listener);
    }
    public void DeRegisterListener(SignalsListener listener)
    {
        listeners.Remove(listener);
    }

}
