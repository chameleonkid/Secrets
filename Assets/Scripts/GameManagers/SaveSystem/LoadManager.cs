using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LoadManager : MonoBehaviour
{
    [SerializeField] private Button saveSlot1;
    [SerializeField] private Button saveSlot2;
    [SerializeField] private Button saveSlot3;
    [SerializeField] private StringValue saveSlot1Text;
    [SerializeField] private StringValue saveSlot2Text;
    [SerializeField] private StringValue saveSlot3Text;
    [SerializeField] private GameObject loadPanel;
    [SerializeField] private GameObject firstButtonPause;

    public void ClickLoadButton()
    {
        loadPanel.SetActive(true);

        var saveNames = SaveUtility.GetSaveNames();
        saveSlot1.GetComponentInChildren<Text>().text = saveNames[0];
        saveSlot2.GetComponentInChildren<Text>().text = saveNames[1];
        saveSlot3.GetComponentInChildren<Text>().text = saveNames[2];
    }

    public void LoadSlot1() => Load(SaveUtility.SaveSlots[0]);

    public void LoadSlot2() => Load(SaveUtility.SaveSlots[1]);

    public void LoadSlot3() => Load(SaveUtility.SaveSlots[2]);

    public void CancelLoad()
    {
        loadPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(firstButtonPause);
    }

    public void Load(string loadSlot)
    {
        SaveManager.Instance.Load(loadSlot);
        loadPanel.SetActive(false);
    }
}
