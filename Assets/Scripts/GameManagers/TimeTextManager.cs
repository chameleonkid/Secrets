using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimeTextManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeDisplay = default;
    [SerializeField] private Image timeImage = default;
    [SerializeField] private Sprite dayTimeSprite = default;
    [SerializeField] private Sprite nightTimeSprite = default;

    private void UpdateUI(float normalizedTimeOfDay)
    {
        var hour = Mathf.FloorToInt(24 * normalizedTimeOfDay);
        var minute = Mathf.FloorToInt((24 * 60 * normalizedTimeOfDay) % 60);
        timeDisplay.text = hour.ToString("00") + ":" + minute.ToString("00");
    }
}
