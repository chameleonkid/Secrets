using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager soundManager;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] attackSounds;
    [SerializeField] private int  rndAttackSound;
    [SerializeField] private AudioClip[] chestSound;

    private void Start()
    {

        soundManager = this;
        audioSource = GetComponent<AudioSource>();
        attackSounds = Resources.LoadAll<AudioClip>("attackSounds");
        chestSound = Resources.LoadAll<AudioClip>("chestSound");
    }

    public void PlayAttackSound()
    {
        rndAttackSound = Random.Range(0, 2);
        audioSource.PlayOneShot(attackSounds[rndAttackSound]);
    }
    public void PlayChestSound()
    {
        audioSource.PlayOneShot(chestSound[0]);
    }

}
