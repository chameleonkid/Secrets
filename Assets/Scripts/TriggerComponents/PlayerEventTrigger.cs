using UnityEngine;

public class PlayerEventTrigger : EventComponentTrigger<PlayerMovement>
{
    protected override void OnTriggerEnter2D(PlayerMovement player)
    {
        Debug.Log("Player enters BossArea!");
        base.OnTriggerEnter2D(player);
    }
}
