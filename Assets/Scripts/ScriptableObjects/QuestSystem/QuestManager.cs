using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public Quest[] mainQuestline;  // Array to hold the quests in the main questline
    public int currentQuestIndex = 0;  // Index to track the current quest in the questline
    public Inventory playerInventory;  // Reference to the player's inventory

    private void Start()
    {
        InitializeQuestProgress();  // Automatically run the initialization at the start
    }

    public void InitializeQuestProgress()  // Made public for external access
    {
        // Iterate through the quests to find the first incomplete one
        while (currentQuestIndex < mainQuestline.Length && mainQuestline[currentQuestIndex].isCompleted)
        {
            currentQuestIndex++;
        }

        if (currentQuestIndex >= mainQuestline.Length)
        {
            Debug.Log("All quests have already been completed.");
        }
        else
        {
            Debug.Log($"Starting with Quest: {mainQuestline[currentQuestIndex].questName}");
        }
    }

    void Update()
    {
        CheckEquipItemObjectives();  // Only need to check equip item objectives in Update
    }

    public void CheckEquipItemObjectives()  // Made public for external access if needed
    {
        if (mainQuestline.Length > currentQuestIndex)
        {
            Quest currentQuest = mainQuestline[currentQuestIndex];

            foreach (var objective in currentQuest.objectives)
            {
                if (!objective.isCompleted && objective.objectiveType == ObjectiveType.EquipItem)
                {
                    if (objective.requiredItem != null && IsItemEquipped(objective.requiredItem))
                    {
                        CompleteObjective(objective);
                    }
                }
            }
        }
    }

    public bool IsItemEquipped(Item item)  // Made public for external access if needed
    {
        // Check if the specified item is currently equipped
        return item == playerInventory.currentWeapon
            || item == playerInventory.currentArmor
            || item == playerInventory.currentHelmet
            || item == playerInventory.currentGloves
            || item == playerInventory.currentLegs
            || item == playerInventory.currentShield
            || item == playerInventory.currentRing
            || item == playerInventory.currentSpellbook
            || item == playerInventory.currentSpellbookTwo
            || item == playerInventory.currentSpellbookThree
            || item == playerInventory.currentAmulet
            || item == playerInventory.currentBoots
            || item == playerInventory.currentLamp
            || item == playerInventory.currentCloak
            || item == playerInventory.currentBelt
            || item == playerInventory.currentShoulder
            || item == playerInventory.currentSeal
            || item == playerInventory.currentSeed
            || item == playerInventory.currentRune
            || item == playerInventory.currentGem
            || item == playerInventory.currentPearl
            || item == playerInventory.currentDragonEgg
            || item == playerInventory.currentArtifact
            || item == playerInventory.currentCrown
            || item == playerInventory.currentScepter;
    }

    public void CompleteObjective(Objective objective)  // This was already public
    {
        if (objective != null && !objective.isCompleted)
        {
            objective.CompleteObjective();
            Debug.Log("Objective Completed: " + objective.description);

            // Check if the current quest is now completed
            mainQuestline[currentQuestIndex].CheckCompletion();
            if (mainQuestline[currentQuestIndex].isCompleted)
            {
                currentQuestIndex++;
                InitializeQuestProgress();  // Re-check for the next incomplete quest
            }
        }
    }
}