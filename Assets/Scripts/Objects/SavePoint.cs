using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    private bool playerInRange;

    void Update()
    {
        if(playerInRange == true && Input.GetButtonDown("Interact"))     // Create new button Interact instead of run
        {
            Save();                 // <--- Instead of directly save you can open your canvas
            Debug.Log("Game was saved!");
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

    public void Save() => SimpleSave.Instance.Save();           //<---- This is how i save (use your own method)

}
