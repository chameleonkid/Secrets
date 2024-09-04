using TMPro;
using UnityEngine;
using UnityEngine.UI; // For Image components

public class QuestUI : MonoBehaviour
{
    public QuestManager questManager;
    public TextMeshProUGUI questText; // Use TextMeshProUGUI for UI text
    public Image objectiveImage; // Add this to display the sprite

    void Update()
    {
        UpdateQuestUI();
    }

    void UpdateQuestUI()
    {
        if (questManager.mainQuestline.Length > questManager.currentQuestIndex)
        {
            Quest currentQuest = questManager.mainQuestline[questManager.currentQuestIndex];
            string questInfo = "<b>Quest:</b> " + currentQuest.questName + "\n<b>Objectives:</b>\n";
            foreach (var objective in currentQuest.objectives)
            {
                questInfo += (objective.isCompleted ? "<color=#00FF00>[Completed]</color>" : "<color=#FF0000>[Incomplete]</color>")
                             + " " + objective.description + "\n";
            }

            questText.text = questInfo;
        }
        else
        {
            questText.text = "<b>All quests completed!</b>";
        }
    }
}