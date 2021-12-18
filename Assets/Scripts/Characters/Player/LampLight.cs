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
        this.enabled = false;
        LumenCheck();
    }

    private void OnEnable()
    {
        player.inventory.OnEquipmentChanged += SetLamp;
        SetLamp();
    }

    private void OnDisable()
    {
        lampLight.intensity = 0;
        player.inventory.OnEquipmentChanged -= SetLamp;
    }

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

    private void SetLamp()
    {
        var lamp = player.inventory.currentLamp;

        if (lamp != null)
        {
            lampLight.intensity = 1;
            lampLight.color = lamp.color;
            lampLight.pointLightOuterRadius = lamp.outerRadius;
            lampLight.pointLightInnerRadius = lamp.innerRadius;
        }
        else
        {
            this.enabled = false;
        }
    }
}
