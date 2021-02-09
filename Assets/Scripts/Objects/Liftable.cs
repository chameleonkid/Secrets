using UnityEngine;

public class Liftable : Interactable
{
    public Item contents;
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private int neededStrenght;
    [SerializeField] private AudioClip liftingSound;
    [SerializeField] private Dialogue dialogue = default;

    private void LateUpdate()
    {
        if (Input.GetButtonDown("Lift") && playerInRange && playerInventory.items.HasCapacity(contents))
        {
            Lifting();
        }
    }

    //############### TEST-Lift ###################################
    public void Lifting()
    {

            if (playerInventory.currentGloves && playerInventory.currentGloves.strength >= neededStrenght)
            {
                playerInventory.items[contents]++;
                Destroy(this.gameObject);
                if(liftingSound)
                {
                    SoundManager.RequestSound(liftingSound);
                }

        }
            else
            {
            DialogueManager.RequestDialogue(dialogue);
            }
      
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
