using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    [SerializeField] private GameObject loadPanel;

    public void NewGame()
    {
        Debug.Log("Scene Laden!");
        SceneManager.LoadScene("StartMenu");
    }

    public void ClickLoadButton() => loadPanel.SetActive(true);

    public void LoadSlot1() => Load(SaveUtility.SaveSlots[0]);

    public void LoadSlot2() => Load(SaveUtility.SaveSlots[1]);

    public void LoadSlot3() => Load(SaveUtility.SaveSlots[2]);

    public void Load(string loadSlot)
    {
        SimpleSave.Instance.Load(loadSlot);
        loadPanel.SetActive(false);
    }

    public void ExitGame() => Application.Quit();
}
