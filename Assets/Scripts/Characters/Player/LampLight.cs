using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class LampLight : MonoBehaviour
{
    private Light2D lampLight;
    private PlayerMovement player;

    private float timer;

    private void Awake()
    {
        lampLight = GetComponent<Light2D>();
        player = GetComponentInParent<PlayerMovement>();

        LumenCheck();
    }

    private void OnEnable()
    {
        var lamp = player.inventory.currentLamp;

        if (lamp != null)
        {
            lampLight.intensity = 1;
            lampLight.color = lamp.color;
            lampLight.pointLightOuterRadius = lamp.outerRadius;
        }
        else
        {
            this.enabled = false;
        }
    }

    private void OnDisable() => lampLight.intensity = 0;

    private void Update()
    {
        timer += Time.deltaTime;

        while (timer >= 1)
        {
            timer--;
            player.lumen.current -= player.inventory.currentLamp.lumenPerSecond;

            LumenCheck();
        }
    }

    private void LumenCheck()
    {
        if (player.lumen.current <= 0)
        {
            this.enabled = false;
            timer = 0;
        }
    }
}
