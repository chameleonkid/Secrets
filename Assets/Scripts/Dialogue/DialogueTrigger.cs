using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public bool playerInRange;
    public GameObject NextButton;

    private void Update()
    {
        if (Input.GetButtonDown("Interact") && playerInRange)
        {
            TriggerDialogue();
            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(NextButton);
        }
        

    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
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
            FindObjectOfType<DialogueManager>().EndDialogue();
            playerInRange = false;
        }
    }

}
