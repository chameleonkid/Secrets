using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue = default;
    private bool playerInRange;

    private void Update()
    {
        if (Input.GetButtonDown("Interact") && playerInRange && Time.timeScale > 0)
        {
            TriggerDialogue();
        }
    }

    public void TriggerDialogue() => DialogueManager.RequestDialogue(dialogue);



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
            DialogueManager.RequestEndDialogue();
            playerInRange = false;
        }
    }
}
