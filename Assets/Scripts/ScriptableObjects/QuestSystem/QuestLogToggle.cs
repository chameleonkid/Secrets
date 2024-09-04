using UnityEngine;

public class QuestLogToggle : MonoBehaviour
{
    public GameObject questLogUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) // Or any other key you'd like
        {
            questLogUI.SetActive(!questLogUI.activeSelf);
        }
    }
}