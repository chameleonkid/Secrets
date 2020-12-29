using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeTextManager : MonoBehaviour
{

    [SerializeField] private WeatherManager weatherManager;
    [SerializeField] private TextMeshProUGUI TimeDisplay = default;

    private void Start() => UpdateUI();

    void Awake()
    {
        weatherManager = GameObject.Find("WeatherManager").GetComponent<WeatherManager>();
    }

    private void FixedUpdate()
    {
        UpdateUI();
    }
    
        private void UpdateUI()
        {
            float currentHour = 24 * weatherManager.GetCurrentTimeOfDay();

            string hours = currentHour.ToString("00.0");
            string hoursTwoDigits = hours.Substring(0, 2);

            float currentMinute =  60 * (currentHour - Mathf.Floor(currentHour));

            string minutes = currentMinute.ToString("00");
            TimeDisplay.text = "" + hoursTwoDigits + ":" + minutes;
         }


}

