using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : MonoBehaviour
{
    private bool playerInRange;
    [SerializeField] private ConstrainedFloat playerHealth;
    [SerializeField] private ConstrainedFloat playerMana;
    [SerializeField] private AudioClip healSound;


    private void Update()
        {
            if (Input.GetButtonDown("Interact") && playerInRange)
            {
                HealPlayer();
            }
        }

    public void HealPlayer()
    {
        if(Time.timeScale > 0)
        {
            Debug.Log("Player gets healed");
            playerHealth.current = playerHealth.max;
            playerMana.current = playerMana.max;
            if (healSound)
            {
                SoundManager.RequestSound(healSound);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.isTrigger)
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.isTrigger)
        {
            playerInRange = false;
        }
    }

}
