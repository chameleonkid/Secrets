using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool playerInRange;
    public Signals contextOn;
    public Signals contextOff;
    [SerializeField] protected PlayerMovement player;

    private void Start()
    {
        player = GameObject.FindObjectOfType<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.isTrigger)
        {
            contextOn.Raise();
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.isTrigger)
        {
            contextOff.Raise();
            playerInRange = false;
        }
    }
}
