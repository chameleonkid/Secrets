using UnityEngine;
using System;

public class MusicManager : MonoBehaviour
{
    private int clipCounter = 0; // Newly added
    private static event Action<AudioClip[]> OnMusicRequested; //<---- Made to array
    [SerializeField] private AudioClip[] currentClips;

    public static void RequestMusic(AudioClip[] music) => OnMusicRequested?.Invoke(music); //<---- Made to array

    private AudioSource audioSource;

    private void OnEnable() => OnMusicRequested += PlayMusic;
    private void OnDisable() => OnMusicRequested -= PlayMusic;

    private void Awake() => audioSource = GetComponent<AudioSource>();


    // New Stuff
    private void Update()
    {
        if(!audioSource.isPlaying)
        {
            PlayMusic(currentClips);
        }
    }
    //*******************************


    private void PlayMusic(AudioClip[] music) //<---- Made to array
    {
        currentClips = music;
        audioSource.clip = music.GetRandomElement(); //<---- Made to Get RandomElement of Array
        audioSource.Play();


        /*
        PSEUDOCODE:
        This needs to be checked over and over again.... In Update?
        IF(AudioClip is over)   <--- Research tells to use audioSource.clip.lenght
        {
        audioSource.clip = music.GetRandomElement();
        }



        */
    }
}
