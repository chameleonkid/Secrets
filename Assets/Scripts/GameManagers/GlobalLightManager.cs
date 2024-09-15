using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Light2D))]
public class GlobalLightManager : MonoBehaviour
{
    private Light2D globalLight = default;
    private float initialIntensity = default;

    private void OnEnable() => TimeManager.OnTimeChanged += UpdateSun;
    private void OnDisable() => TimeManager.OnTimeChanged -= UpdateSun;

    private void Awake()
    {
        globalLight = GetComponent<Light2D>();
        initialIntensity = globalLight.intensity;
    }

    private void UpdateSun(float normalizedTimeOfDay)
    {
        if (SceneManager.GetActiveScene().name.Contains("Overworld") || SceneManager.GetActiveScene().name.Contains("Test"))
        {
            float intensityMultiplier = 1;
            if (normalizedTimeOfDay <= 0.23f || normalizedTimeOfDay >= 0.75f)
            {
                intensityMultiplier = 0.124f;         // 0 = absolute Darkness 
            }
            else if (normalizedTimeOfDay <= 0.3f)
            {
                intensityMultiplier = Mathf.Clamp01((normalizedTimeOfDay - 0.23f) * (1 / 0.02f));
            }
            else if (normalizedTimeOfDay >= 0.73f)
            {
                intensityMultiplier = Mathf.Clamp01(1 - ((normalizedTimeOfDay - 0.73f) * (1 / 0.02f)));
            }

            globalLight.intensity = initialIntensity * intensityMultiplier;
        }
        else if (SceneManager.GetActiveScene().name.Contains("Dungeon") || SceneManager.GetActiveScene().name.Contains("Cave"))
        {
            globalLight.intensity = 0.03f;   // Low light for dungeons and caves
        }
        else if (SceneManager.GetActiveScene().name.Contains("ShipIntro"))
        {
            globalLight.intensity = 0.05f;   // Slightly dim light for the ship intro
        }
        else if (SceneManager.GetActiveScene().name.Contains("Indoor"))
        {
            globalLight.intensity = 0.3f;    // Medium dim light for indoor castle scenes
        }
        else
        {
            globalLight.intensity = 1;       // Full brightness for other scenes
        }
    }
}