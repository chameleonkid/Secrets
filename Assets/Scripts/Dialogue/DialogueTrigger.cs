using UnityEngine;

public class DialogueTrigger : ComponentTrigger<PlayerMovement>
{
    protected override bool? needOtherIsTrigger => true;

    [SerializeField] private Dialogue dialogue = default;
    [SerializeField] private QuestManager questManager;  // Reference to the QuestManager
    [SerializeField] private Objective objective;        // The objective to complete when this dialogue is triggered (optional)

    private PlayerMovement player;

    private void LateUpdate()
    {
        if (player != null && player.inputInteract && Time.timeScale > 0)
        {
            Debug.Log("Interaction" + player.inputInteract);
            TriggerDialogue();
        }
    }

    public void TriggerDialogue()
    {
        DialogueManager.RequestDialogue(dialogue);

        // Complete the objective if one is assigned
        if (objective != null && questManager != null)
        {
            questManager.CompleteObjective(objective);
        }
    }

    protected override void OnEnter(PlayerMovement player)
    {
        this.player = player;
    }

    protected override void OnExit(PlayerMovement player)
    {
        DialogueManager.RequestEndDialogue();
        this.player = null;
    }
}