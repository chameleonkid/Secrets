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
        if(player.GetInteraction())
        {
            Debug.Log("Interaction" + player.GetInteraction());
        }
        if ((Input.GetButtonDown("Interact") || player.GetInteraction()) && playerInRange && Time.timeScale > 0)
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
