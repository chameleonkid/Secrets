using UnityEngine;
using System;

public class MusicManager : MonoBehaviour
{
    private static event Action<AudioClip> OnMusicRequested;

    public static void RequestMusic(AudioClip music) => OnMusicRequested?.Invoke(music);

    private AudioSource audioSource;

    private void OnEnable() => OnMusicRequested += PlayMusic;
    private void OnDisable() => OnMusicRequested -= PlayMusic;

    private void Awake() => audioSource = GetComponent<AudioSource>();

    private void PlayMusic(AudioClip music)
    {
        audioSource.clip = music;
        audioSource.Play();
    }
}
