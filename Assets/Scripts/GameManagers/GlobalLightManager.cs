using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Light2D))]
public class GlobalLightManager : MonoBehaviour
{
    [SerializeField] private Light2D globalLight = default;
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
        if (SceneManager.GetActiveScene().name == "Overworld")
        {
            float intensityMultiplier = 1;
            if (normalizedTimeOfDay <= 0.23f || normalizedTimeOfDay >= 0.75f)
            {
                intensityMultiplier = 0;
            }
            else if (normalizedTimeOfDay <= 0.25f)
            {
                intensityMultiplier = Mathf.Clamp01((normalizedTimeOfDay - 0.23f) * (1 / 0.02f));
            }
            else if (normalizedTimeOfDay >= 0.73f)
            {
                intensityMultiplier = Mathf.Clamp01(1 - ((normalizedTimeOfDay - 0.73f) * (1 / 0.02f)));
            }

            globalLight.intensity = initialIntensity * intensityMultiplier;
        }
        else if (SceneManager.GetActiveScene().name.Contains("Dungeon"))
        {
            globalLight.intensity = 0;
        }
        else
        {
            globalLight.intensity = 1;
        }
    }
}
