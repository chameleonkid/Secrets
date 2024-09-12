using UnityEngine;
using UnityEditor;
using System.IO;

public class QuestResetEditor : Editor
{
    // Path to the parent folder where the quest and objective ScriptableObjects are stored
    private static readonly string questFolderPath = "Assets/ScriptableObjects/SaveObjects/Quests";

    [MenuItem("Tools/Reset All Quests and Objectives")]
    public static void ResetAllQuestsAndObjectives()
    {
        // Reset all Quest ScriptableObjects
        ResetScriptableObjects<Quest>(questFolderPath);

        // Reset all Objective ScriptableObjects
        ResetScriptableObjects<Objective>(questFolderPath);

        AssetDatabase.SaveAssets(); // Save all changes
        Debug.Log("All quests and objectives have been reset.");
    }

    private static void ResetScriptableObjects<T>(string folderPath) where T : ScriptableObject
    {
        // Find all ScriptableObjects of type T in the specified folder
        string[] assetPaths = Directory.GetFiles(folderPath, "*.asset", SearchOption.AllDirectories);

        foreach (string path in assetPaths)
        {
            T asset = AssetDatabase.LoadAssetAtPath<T>(path);
            if (asset != null)
            {
                if (typeof(T) == typeof(Quest))
                {
                    ResetQuest(asset as Quest);
                }
                else if (typeof(T) == typeof(Objective))
                {
                    ResetObjective(asset as Objective);
                }
                EditorUtility.SetDirty(asset); // Mark the object as dirty so Unity knows it has changed
            }
        }
    }

    private static void ResetQuest(Quest quest)
    {
        // Reset the quest itself
        quest.isCompleted = false;
        Debug.Log($"Quest '{quest.questName}' has been reset.");
    }

    private static void ResetObjective(Objective objective)
    {
        // Reset the objective itself
        objective.isCompleted = false;
        Debug.Log($"Objective '{objective.description}' has been reset.");
    }
}