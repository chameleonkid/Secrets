using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicRequester : MonoBehaviour
{
    [SerializeField] private AudioClip areaMusic = default;
    [SerializeField] private Collider2D areaMusicCollider = default;


    private void OnTriggerEnter2D(Collider2D other)
    {
        Character player = other.GetComponent<PlayerMovement>();
        if (player != null)
        {
            MusicManager.RequestMusic(areaMusic);
        }
    }

}



