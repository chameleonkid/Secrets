using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    private static event Action<AudioClip, bool> OnSoundRequested; // loop flag is now part of the event
    public static void RequestSound(AudioClip sound, bool loop = false) => OnSoundRequested?.Invoke(sound, loop); // Optional loop parameter

    private AudioSource audioSource;

    private void OnEnable() => OnSoundRequested += PlaySound;
    private void OnDisable() => OnSoundRequested -= PlaySound;

    private void Awake() => audioSource = GetComponent<AudioSource>();

    private void PlaySound(AudioClip sound, bool loop)
    {
        if (sound == null)
        {
            // Stop the sound if no clip is provided
            audioSource.Stop();
            return;
        }

        audioSource.loop = loop;  // Set loop
        if (loop)
        {
            audioSource.clip = sound;
            audioSource.Play();  // Play looped sound
        }
        else
        {
            audioSource.PlayOneShot(sound);  // Play one-shot sound
        }
    }
}