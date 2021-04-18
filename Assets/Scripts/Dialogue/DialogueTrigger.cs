using UnityEngine;

public class DialogueTrigger : ComponentTrigger<PlayerMovement>
{
    protected override bool? needOtherIsTrigger => true;

    [SerializeField] private Dialogue dialogue = default;
    private PlayerMovement player;

    private void Update()
    {
        if (player != null && player.inputInteract && Time.timeScale > 0)
        {
            Debug.Log("Interaction" + player.inputInteract);
            TriggerDialogue();
        }
    }

    public void TriggerDialogue() => DialogueManager.RequestDialogue(dialogue);

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
