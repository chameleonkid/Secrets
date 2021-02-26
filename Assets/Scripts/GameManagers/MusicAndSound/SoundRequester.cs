using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundRequester : MonoBehaviour
{
    [SerializeField] private AudioClip soundToRequest1;
    [SerializeField] private AudioClip soundToRequest2;
    [SerializeField] private AudioClip soundToRequest3;

    public void RequestSound1()
    {
        SoundManager.RequestSound(soundToRequest1);
    }
    public void RequestSound2()
    {
        SoundManager.RequestSound(soundToRequest2);
    }

    public void RequestSound3()
    {
        SoundManager.RequestSound(soundToRequest3);
    }
}
