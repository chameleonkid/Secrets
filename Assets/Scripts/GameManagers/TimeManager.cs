using System;
using UnityEngine;

public class TimeManager : DDOLSingleton<TimeManager>
{
    public static event Action<float> OnTimeChanged;

    [SerializeField] private float fullDayLength = 60;
    [SerializeField] private float timeMultiplier = 1;

    [SerializeField] private float normalizedTimeOfDay = 0;

    private void Update()
    {
        normalizedTimeOfDay += (Time.deltaTime / fullDayLength) * timeMultiplier;
        while (normalizedTimeOfDay >= 1)
        {
            normalizedTimeOfDay--;
        }

        OnTimeChanged?.Invoke(normalizedTimeOfDay);
    }
}
