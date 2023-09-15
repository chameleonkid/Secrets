using UnityEngine;

public class StarGateActivation : Interactable
{
    // Reference to the BoolValue ScriptableObject for this gate
    public BoolValue isActivated;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private  Sprite activeSprite;
    [SerializeField] private Sprite inActiveSprite;
    [SerializeField] private GameObject starGatePanel;
    [SerializeField] private StargateButtonMenu stargateMenu;



    // Collider2D reference for the Stargate
    private Collider2D stargateCollider;

    private void Awake()
    {
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
        // Get the Collider2D component
        if(isActivated.RuntimeValue)
        {
            spriteRenderer.sprite = activeSprite;
        }
        else
        {
            spriteRenderer.sprite = inActiveSprite;
        }
        stargateCollider = GetComponent<Collider2D>();
    }

    private void LateUpdate()
    {
        if (playerInRange == true && player.inputInteract && CanvasManager.Instance.IsFreeOrActive(starGatePanel) && Time.timeScale > 0)
        {
            //refresh all Buttons in Startgate UI
            stargateMenu.ActivateButtons();

            if (starGatePanel != null)
            {
                Debug.Log("Update: starGatePanel not null");

                starGatePanel.SetActive(true);
                Time.timeScale = 0f;
            }

        }
    }

    protected override void OnEnter(PlayerMovement player)
    {
            // Check if the gate is not already activated
            if (!isActivated.RuntimeValue)
            {
                // Mark the gate as activated
                isActivated.RuntimeValue = true;
                spriteRenderer.sprite = activeSprite;
                // Optionally, play an activation animation or sound

            }
        this.player = player;
        Debug.Log("Player war set");
    }

}