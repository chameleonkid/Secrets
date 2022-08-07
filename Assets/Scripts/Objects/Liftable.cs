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
        if (playerInRange && Input.GetButtonDown("Lift") && Time.timeScale > 0)
        {
            if (playerInventory.items.HasCapacity(contents))
            {
                Lifting();
            }
            else
            {
                NoInventorySpace();
            }
        }
    }

    //############### TEST-Lift ###################################
    public void Lifting()
    {
        if (playerInventory.currentGloves && playerInventory.currentGloves.strength >= neededStrenght)
        {
            playerInventory.items[contents]++;
            Destroy(this.gameObject);
            if (liftingSound)
            {
                SoundManager.RequestSound(liftingSound);
            }
        }
        else
        {
            dialogue.lines[0].text = "You don't seem strong enaugh to lift this up...";
            DialogueManager.RequestDialogue(dialogue);
        }
    }

    public void NoInventorySpace()
    {
        dialogue.lines[0].text = "There is no space left in your inventory";
        TriggerDialogue();
    }

    public void TriggerDialogue() => DialogueManager.RequestDialogue(dialogue);
}
