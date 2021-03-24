using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : MonoBehaviour
{
    private bool playerInRange;
    [SerializeField] private ConstrainedFloat playerHealth;
    [SerializeField] private ConstrainedFloat playerMana;

    private void Update()
        {
            if (Input.GetButtonDown("Interact") && playerInRange && Time.timeScale > 0)
            {
                HealPlayer();
            }
        }

    public void HealPlayer()
    {
        playerHealth.current = playerHealth.max;
        playerMana.current = playerMana.max;
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
