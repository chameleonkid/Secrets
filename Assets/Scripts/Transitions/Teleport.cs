using System.Collections;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Vector3 playerChange;
    [SerializeField] private float transitionTime = 2f;
    private PlayerMovement player;
    [SerializeField] private Animator anim = default;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerMovement>() && !other.isTrigger)
        {
            player = other.GetComponent<PlayerMovement>();
            StartCoroutine(StartTeleportCo());
        }
    }

    private IEnumerator StartTeleportCo()
    {
        player.GetComponent<PlayerMovement>().LockMovement(transitionTime + 2f);
        anim.SetTrigger("StartLoading");
        yield return new WaitForSeconds(1.5f);
        player.transform.position = playerChange;
        yield return new WaitForSeconds(transitionTime);
        anim.SetTrigger("StopLoading");
    }
}
