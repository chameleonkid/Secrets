using System.Collections;
using Schwer.States;
using UnityEngine;

public class Teleport : ComponentTrigger<PlayerMovement>
{
    public Vector3 playerChange;
    [SerializeField] private float transitionTime = 2f;
    [SerializeField] private Animator anim = default;

    protected override void OnEnter(PlayerMovement player)
    {
        StartCoroutine(TeleportCo(player));
    }

    private IEnumerator TeleportCo(PlayerMovement player)
    {
        player.currentState = new Locked(player, transitionTime + 2f);
        anim.SetTrigger("StartLoading");
        yield return new WaitForSeconds(1.5f);
        player.transform.position = playerChange;
        yield return new WaitForSeconds(transitionTime);
        anim.SetTrigger("StopLoading");
    }
}
