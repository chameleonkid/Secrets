using UnityEngine;
using System;

public class SoundManager : MonoBehaviour
{
    private static event Action<AudioClip> OnSoundRequested;
    public static void RequestSound(AudioClip sound) => OnSoundRequested?.Invoke(sound);

    private AudioSource audioSource;

    private void OnEnable() => OnSoundRequested += PlaySound;
    private void OnDisable() => OnSoundRequested -= PlaySound;

    private void Awake() => audioSource = GetComponent<AudioSource>();

    private void PlaySound(AudioClip sound) => audioSource.PlayOneShot(sound);
}
