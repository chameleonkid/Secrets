using UnityEngine;

[CreateAssetMenu(fileName = "NewQuest", menuName = "Quest/MainQuest")]
public class Quest : ScriptableObject
{
    public string questName;
    public Objective[] objectives;
    public bool isCompleted;

    public void CheckCompletion()
    {
        isCompleted = true;
        foreach (var objective in objectives)
        {
            if (!objective.isCompleted)
            {
                isCompleted = false;
                break;
            }
        }
    }
}