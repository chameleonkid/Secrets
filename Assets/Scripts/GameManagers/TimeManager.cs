using System;
using UnityEngine;

public class TimeManager : DDOLSingleton<TimeManager>
{
    public static event Action<float> OnTimeChanged;

    [SerializeField] private float fullDayLength = 60;
    [SerializeField] private float timeMultiplier = 1;
    [SerializeField] private FloatValue _normalizedTimeOfDay = default;
    private float normalizedTimeOfDay { get => _normalizedTimeOfDay.value; set => _normalizedTimeOfDay.value = value; }

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
