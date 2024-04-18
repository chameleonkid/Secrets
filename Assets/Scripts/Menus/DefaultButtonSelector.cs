using UnityEngine;
using UnityEngine.EventSystems;

public class DefaultButtonSelector : MonoBehaviour
{
    public EventSystem eventSystem;
    public GameObject selectedObject;

    private void Start()
    {
        eventSystem.SetSelectedGameObject(null);
        eventSystem.SetSelectedGameObject(selectedObject);
    }
}