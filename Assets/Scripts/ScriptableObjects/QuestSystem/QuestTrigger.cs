using UnityEngine;

public class QuestTrigger : MonoBehaviour
{
    public QuestManager questManager;
    public Objective objectiveToComplete;

    private void Awake()
    {
        questManager = FindObjectOfType<QuestManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Assuming the player has the tag "Player"
        {
            questManager.CompleteObjective(objectiveToComplete);
            Debug.Log("Objective Completed: " + objectiveToComplete.description);
        }
    }
}