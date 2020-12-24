using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ColliderTrigger : MonoBehaviour
{
    public event Action OnPlayerEnterTrigger;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Character player = other.GetComponent<PlayerMovement>();
        if (player != null) 
        {
            //Its the player inside the triggerArea
            Debug.Log("Player enters BossArea!");
            OnPlayerEnterTrigger?.Invoke();
        }
    }
}
