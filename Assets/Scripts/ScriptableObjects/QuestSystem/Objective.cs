using UnityEngine;

public enum ObjectiveType
{
    FindPlace,
    EquipItem,
    TalkToPerson,
    KillEnemy
}

[CreateAssetMenu(fileName = "New Objective", menuName = "Quest/Objective")]
public class Objective : ScriptableObject
{
    public string description;
    public ObjectiveType objectiveType;  // Type of the objective
    public Item requiredItem;  // The item that must be equipped (for EquipItem objectives)

    public bool isCompleted;

    public void CompleteObjective()
    {
        isCompleted = true;
    }
}
