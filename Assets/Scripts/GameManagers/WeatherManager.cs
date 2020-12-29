using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;



public class WeatherManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem snowMaker;
    [SerializeField] private ParticleSystem rainMaker;
    [SerializeField] private ParticleSystem.EmissionModule snowMakerEmission;
    [SerializeField] private ParticleSystem.EmissionModule rainMakerEmission;
    [SerializeField] private bool isRaining = false;
    [SerializeField] private bool isSunny = false;
    [SerializeField] private bool isSnowing = false;

    [SerializeField] private Light2D globalLight; // I could use Light instead of GameObject but how can i "find it"
    [SerializeField] private float globalLightInitialIntensity;
    [SerializeField] private float intensityMultiplier = 1f;
    [SerializeField] private float secondsInFullDay = 60;
    [SerializeField] private float currentTimeOfDay = 0;
    [SerializeField] private float currentTimeOfDayNotNormalized;
    [SerializeField] private float timeMultiplier = 1f;

    public void Update()
    {
        UpdateSun();

        currentTimeOfDay += (Time.deltaTime / secondsInFullDay) * timeMultiplier;       //Normalizing to get the full-day-night-cycle
        if (currentTimeOfDay >= 1)                                                      //If it reaches the full day reset to 0
        {
            currentTimeOfDay = 0;
        }

        if (isRaining)
        {
            StopCoroutine(Snow());
            StopCoroutine(Sunny());
            StartRain();
        }
        else if(isSnowing)
        {
            StopCoroutine(Rain());
            StopCoroutine(Sunny());
            StartSnow();
        }
        else if(isSunny)
        {
            StopCoroutine(Rain());
            StopCoroutine(Sunny());
            StartSunny();
        }
        else
        {
            return;
        }
        
    }

    public void Awake()
    {
        globalLight = GameObject.Find("Global Light 2D").GetComponent<Light2D>();
        rainMaker = GameObject.Find("RainMaker").GetComponent<ParticleSystem>();
        snowMaker = GameObject.Find("SnowMaker").GetComponent<ParticleSystem>();
        snowMakerEmission = snowMaker.emission;
        rainMakerEmission = rainMaker.emission;
        globalLightInitialIntensity = globalLight.intensity;
      //  StartSnow(); //For Testing
    }

    public void StartSnow()
    {
        StartCoroutine(Snow());
    }
    public void StartRain()
    {
        StartCoroutine(Rain());

    }
    public void StartSunny()
    {
        StartCoroutine(Sunny());
    }


    private IEnumerator Snow()
    {
        rainMakerEmission.rateOverTime = 0f;
        isRaining = false;
        isSunny = false;
        yield return new WaitForSeconds(2f);
        snowMakerEmission.rateOverTime = 20f;
        yield return new WaitForSeconds(5f);
        snowMakerEmission.rateOverTime = 50f;
        yield return new WaitForSeconds(10f);
        snowMakerEmission.rateOverTime = 100f;
        yield return new WaitForSeconds(5f);
        snowMakerEmission.rateOverTime = 200f;
    }

    private IEnumerator Rain()
    {
        isSnowing = false;
        isSunny = false;
        snowMakerEmission.rateOverTime = 0f;
        yield return new WaitForSeconds(2f);
        rainMakerEmission.rateOverTime = 20f;
        yield return new WaitForSeconds(5f);
        rainMakerEmission.rateOverTime = 50f;
        yield return new WaitForSeconds(10f);
        rainMakerEmission.rateOverTime = 100f;
        yield return new WaitForSeconds(5f);
        rainMakerEmission.rateOverTime = 200f;
    }

    private IEnumerator Sunny()
    {
        isSnowing = false;
        isRaining = false;
        snowMakerEmission.rateOverTime = 0f;
        yield return new WaitForSeconds(2f);
        rainMakerEmission.rateOverTime = 0f;
        yield return new WaitForSeconds(5f);
    }


    void UpdateSun()
    {
        float intensityMultiplier = 1;
        if (currentTimeOfDay <= 0.23f || currentTimeOfDay >= 0.75f)
        {
            intensityMultiplier = 0;
        }
        else if (currentTimeOfDay <= 0.25f)
        {
            intensityMultiplier = Mathf.Clamp01((currentTimeOfDay - 0.23f) * (1 / 0.02f));
        }
        else if (currentTimeOfDay >= 0.73f)
        {
            intensityMultiplier = Mathf.Clamp01(1 - ((currentTimeOfDay - 0.73f) * (1 / 0.02f)));
        }

        globalLight.intensity = globalLightInitialIntensity * intensityMultiplier;
    }


    public float GetCurrentTimeOfDay()
    {
        return currentTimeOfDay;
    }


}
