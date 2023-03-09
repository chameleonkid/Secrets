using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonSounds : MonoBehaviour, ISelectHandler, ISubmitHandler
{
    [SerializeField] private AudioClip buttonSelectSound = default;
    [SerializeField] private AudioClip buttonPressSound = default;

    public void OnSelect(BaseEventData eventData)
    {
        SoundManager.RequestSound(buttonSelectSound);
    }

    public void OnSubmit(BaseEventData eventData)
    {
        SoundManager.RequestSound(buttonPressSound);
    }

}