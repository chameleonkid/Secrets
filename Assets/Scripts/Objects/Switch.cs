using Schwer.States;
using UnityEngine;

public class Switch : ComponentTrigger<PlayerMovement>
{
    protected override bool? needOtherIsTrigger => null;

    public bool active;
    public BoolValue storeValue;
    public Sprite activeSprite;
    private SpriteRenderer mySprite;
    public Door thisDoor;
    [Header("Sounds")]
    [SerializeField] private AudioClip switchSound;

    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
        active = storeValue.RuntimeValue;
        if (active)
        {
            ActivateSwitch();
        }
    }

    private void ActivateSwitch()
    {
        SoundManager.RequestSound(switchSound);
        active = true;
        storeValue.RuntimeValue = active;
        thisDoor.Open();
        mySprite.sprite = activeSprite;
    }

    protected override void OnEnter(PlayerMovement player)
    {
        if (!active)
        {
            player.currentState = new Locked(player, 0.25f);
            ActivateSwitch();
        }
    }
}
