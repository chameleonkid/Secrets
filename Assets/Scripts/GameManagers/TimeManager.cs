using System;
using UnityEngine;

public class TimeManager : DDOLSingleton<TimeManager>
{
    public static event Action<float> OnTimeChanged;

    [SerializeField] private float fullDayLength = 60;
    [SerializeField] private float timeMultiplier = 1;
    [SerializeField] private FloatValue timeSaver = default;
    [SerializeField] private float normalizedTimeOfDay = 0;


    void OnEnable()
    {
        SimpleSave.OnDataLoaded += SetTime;
    }

    void OnDisable()
    {
        SimpleSave.OnDataLoaded -= SetTime;
    }

    private void Update()
    {
        normalizedTimeOfDay += (Time.deltaTime / fullDayLength) * timeMultiplier;
        while (normalizedTimeOfDay >= 1)
        {
            normalizedTimeOfDay--;
        }

        OnTimeChanged?.Invoke(normalizedTimeOfDay);
        timeSaver.value = normalizedTimeOfDay;
    }

    private void SetTime()
    {
        normalizedTimeOfDay = timeSaver.value;
    }
}
