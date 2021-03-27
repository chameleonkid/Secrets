using UnityEngine;

public class PlayerEventTrigger : EventComponentTrigger<PlayerMovement>
{
    protected override void OnEnter(PlayerMovement player)
    {
        Debug.Log("Player enters BossArea!");
        base.OnEnter(player);
    }
}
