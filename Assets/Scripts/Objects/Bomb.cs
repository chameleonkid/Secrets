using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private AudioClip explosionSound;
    [SerializeField] private AudioClip tickingSound;
    [SerializeField] private AudioClip bombDroppedSound;


    void ExplosionSound()
    {
        SoundManager.RequestSound(explosionSound);
    }
    void TickingSoundSound()
    {
        SoundManager.RequestSound(tickingSound);
    }
    void BombDroppedSound()
    {
        SoundManager.RequestSound(bombDroppedSound);
    }

}
