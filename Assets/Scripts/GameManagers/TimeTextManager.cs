using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeTextManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI timeDisplay = default;

    [SerializeField] private float fullDayLength = 60;
    [SerializeField] private float timeMultiplier = 1;


    [SerializeField] private float _normalizedTimeOfDay = 0;
    public float normalizedTimeOfDay => _normalizedTimeOfDay;

    private void Start() => UpdateUI();

    void Awake()
    {
        timeDisplay = GameObject.Find("TimeText").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        UpdateTime();  //! New
        UpdateUI();
    }

    private void UpdateTime()
    {
        _normalizedTimeOfDay += (Time.deltaTime / fullDayLength) * timeMultiplier;
        if (_normalizedTimeOfDay >= 1)
        {
            _normalizedTimeOfDay = 0;
        }
    }


    private void UpdateUI()
    {
        var hour = Mathf.FloorToInt(24 * normalizedTimeOfDay);
        var minute = Mathf.FloorToInt((24 * 60 * normalizedTimeOfDay) % 60);
        timeDisplay.text = hour.ToString("00") + ":" + minute.ToString("00");
    }


}

