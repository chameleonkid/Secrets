using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class LampLight : MonoBehaviour
{
    private Light2D lampLight;
    private PlayerMovement player;
    [SerializeField] private InventoryManager inventoryManager;

    private float timer;

    private void Awake()
    {
        lampLight = GetComponent<Light2D>();
        player = GetComponentInParent<PlayerMovement>();

        LumenCheck();
    }

    private void OnEnable()
    {
        inventoryManager.OnEquipItem += InventoryManager_OnEquipItem;
        setLamp();
    }

    private void InventoryManager_OnEquipItem()
    {
        setLamp();
    }

    private void OnDisable()
    {
        lampLight.intensity = 0;
        inventoryManager.OnEquipItem -= InventoryManager_OnEquipItem;
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

    private void setLamp()
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
}
