using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue = default;
    private bool playerInRange;
    private PlayerMovement player;

    private void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMovement>();
    }

    private void Update()
    {
        if (player.inputInteract && playerInRange && Time.timeScale > 0)
        {
            Debug.Log("Interaction" + player.inputInteract);
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
