using UnityEngine;

public class PlayerEventTrigger : ComponentEventTrigger<PlayerMovement>
{
    protected override bool? needOtherIsTrigger => null;

    protected override void OnEnter(PlayerMovement player)
    {
        Debug.Log("Player enters BossArea!");
        base.OnEnter(player);
    }
}
