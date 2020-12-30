using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeTextManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI TimeDisplay = default;
    [SerializeField] private float seconds = 0;
    [SerializeField] private float multiplier = 1;
    [SerializeField] private int minutes = 0;
    [SerializeField] private int hours = 0;

    private void Start() => UpdateUI();

    private void FixedUpdate()
    {
        UpdateUI();
    }
    
    private void UpdateUI()
    {
    seconds += Time.fixedDeltaTime;
    if (seconds >= multiplier)
    {
        seconds = 0;
        minutes++;
        if(minutes > 59)
        {
            minutes = 0;
            hours++;
            if(hours > 23)
            {
                hours = 0;
            }
        }
    }
    string hoursString = hours.ToString("00");
    string minutesString = minutes.ToString("00");
    TimeDisplay.text = "" + hoursString + ":" + minutesString;
    }

    public int GetHours()
    {
        return hours;
    }

    public int GetMinutes()
    {
        return hours;
    }

    public float GetMultiplier()
    {
        return multiplier;
    }
}

