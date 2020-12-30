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

    [SerializeField] private Light2D globalLight;
    [SerializeField] private float globalLightInitialIntensity;
    [SerializeField] private float normalizedTimeOfDay = 0;
    [SerializeField] private TimeTextManager time = default;


    public void Update()
    {
        UpdateSun();

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
        time = GameObject.Find("TimeInfo").GetComponent<TimeTextManager>();
        
        snowMakerEmission = snowMaker.emission;
        rainMakerEmission = rainMaker.emission;
        globalLightInitialIntensity = globalLight.intensity;
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
        var normTime = time.normalizedTimeOfDay;

        float intensityMultiplier = 1;
        if (normTime <= 0.23f || normTime >= 0.75f)
        {
            intensityMultiplier = 0;
        }
        else if (normTime <= 0.25f)
        {
            intensityMultiplier = Mathf.Clamp01((normTime - 0.23f) * (1 / 0.02f));
        }
        else if (normTime >= 0.73f)
        {
            intensityMultiplier = Mathf.Clamp01(1 - ((normTime - 0.73f) * (1 / 0.02f)));
        }

        globalLight.intensity = globalLightInitialIntensity * intensityMultiplier;
    }

}
