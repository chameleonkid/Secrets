using UnityEngine;
using System;

public class MusicManager : MonoBehaviour
{
    //private int clipCounter = 0; // Newly added
    private static event Action<AudioClip[]> OnMusicRequested; //<---- Made to array
    [SerializeField] private AudioClip[] currentClips;

    public static void RequestMusic(AudioClip[] music) => OnMusicRequested?.Invoke(music); //<---- Made to array

    private AudioSource audioSource;

    private void OnEnable() => OnMusicRequested += PlayMusic;
    private void OnDisable() => OnMusicRequested -= PlayMusic;

    private void Awake() => audioSource = GetComponent<AudioSource>();



    private void Update()
    {
        if(!audioSource.isPlaying)
        {
            PlayMusic(currentClips);
        }
    }
    //*******************************


    private void PlayMusic(AudioClip[] music)
    {
        currentClips = music;
        audioSource.clip = music.GetRandomElement();
        audioSource.Play();
    }
}
