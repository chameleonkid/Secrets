using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class HoldableButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{   // Adapted from: https://stackoverflow.com/questions/55448551/unity3d-how-to-detect-when-a-button-is-being-held-down-and-released
    private bool held;

    private Button button;

    private void Awake() => button = GetComponent<Button>();

    private void Update()
    {
        if (!held) return;
        if (!button.IsInteractable()) held = false;

        button.onClick.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (button.IsInteractable())
        {
            held = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (button.IsInteractable())
        {
            held = false;
        }
    }
}
