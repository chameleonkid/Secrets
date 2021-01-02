using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private Light2D globalLight;
    [SerializeField] private float globalLightInitialIntensity;
    [SerializeField] private float normalizedTimeOfDay = 0;
    [SerializeField] private TimeTextManager time = default;

    [SerializeField] private Sprite dayTimeSprite;
    [SerializeField] private Sprite nightTimeSprite;
    [SerializeField] private Image timeImage;

    private void Update()
    {
        UpdateSun();
    }

    private void Awake()
    {
        globalLight = GameObject.Find("Global Light 2D").GetComponent<Light2D>();
        time = GameObject.Find("TimeInfo").GetComponent<TimeTextManager>();
        timeImage = GameObject.Find("TimeInfo").GetComponent<Image>();
        globalLightInitialIntensity = globalLight.intensity;
    }

    private void UpdateSun()
    {
        if (SceneManager.GetActiveScene().name == "Overworld")
        {
            var normTime = time.normalizedTimeOfDay;

            float intensityMultiplier = 1;
            if (normTime <= 0.23f || normTime >= 0.75f)
            {
                intensityMultiplier = 0;
                timeImage.sprite = nightTimeSprite;
            }
            else if (normTime <= 0.25f)
            {
                intensityMultiplier = Mathf.Clamp01((normTime - 0.23f) * (1 / 0.02f));
                timeImage.sprite = dayTimeSprite;
            }
            else if (normTime >= 0.73f)
            {
                intensityMultiplier = Mathf.Clamp01(1 - ((normTime - 0.73f) * (1 / 0.02f)));
                timeImage.sprite = nightTimeSprite;
            }

            globalLight.intensity = globalLightInitialIntensity * intensityMultiplier;
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
