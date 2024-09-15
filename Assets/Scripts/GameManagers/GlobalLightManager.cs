using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Light2D))]
public class GlobalLightManager : MonoBehaviour
{
    private Light2D globalLight = default;
    private float initialIntensity = default;

    [Header("Scene Intensity Multipliers")]
    [Range(0, 1)] public float shipIntroIntensity = 0.05f;
    [Range(0, 1)] public float defaultIntensity = 0.3f;

    private void OnEnable() => TimeManager.OnTimeChanged += UpdateSun;
    private void OnDisable() => TimeManager.OnTimeChanged -= UpdateSun;

    private void Awake()
    {
        globalLight = GetComponent<Light2D>();
        initialIntensity = globalLight.intensity;
    }

    private void UpdateSun(float normalizedTimeOfDay)
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName.Contains("ShipIntro"))
        {
            // Fixed intensity for the Ship Intro scene
            globalLight.intensity = shipIntroIntensity;
        }
        else if (sceneName.Contains("Overworld"))
        {
            // Dynamic intensity based on time of day for the Overworld scene
            float intensityMultiplier = 1;

            if (normalizedTimeOfDay <= 0.23f || normalizedTimeOfDay >= 0.75f)
            {
                intensityMultiplier = 0.124f;  // Near darkness during night
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
        else
        {
            // Adjustable intensity for all other scenes
            globalLight.intensity = defaultIntensity;
        }
    }
}