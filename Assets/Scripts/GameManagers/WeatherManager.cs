using System.Collections;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem snowMaker;
    [SerializeField] private ParticleSystem rainMaker;
    [SerializeField] private ParticleSystem.EmissionModule snowMakerEmission;
    [SerializeField] private ParticleSystem.EmissionModule rainMakerEmission;
    [SerializeField] private bool isRaining = false;
    [SerializeField] private bool isSunny = false;
    [SerializeField] private bool isSnowing = false;

    private void Update()
    {
        if (isRaining)
        {
            StopCoroutine(Snow());
            StopCoroutine(Sunny());
            StartRain();
        }
        else if (isSnowing)
        {
            StopCoroutine(Rain());
            StopCoroutine(Sunny());
            StartSnow();
        }
        else if (isSunny)
        {
            StopCoroutine(Rain());
            StopCoroutine(Sunny());
            StartSunny();
        }
    }

    private void Awake()
    {
        rainMaker = GameObject.Find("RainMaker").GetComponent<ParticleSystem>();
        snowMaker = GameObject.Find("SnowMaker").GetComponent<ParticleSystem>();
        snowMakerEmission = snowMaker.emission;
        rainMakerEmission = rainMaker.emission;
    }

    public void StartSnow() => StartCoroutine(Snow());

    public void StartRain() => StartCoroutine(Rain());

    public void StartSunny() => StartCoroutine(Sunny());

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
}
