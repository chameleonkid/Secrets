using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SzeneTransition : ComponentTrigger<PlayerMovement>
{
    [Header("New Scene Variables")]
    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerPosMemory;
    [SerializeField] private float transitionTime = 3f;
    [SerializeField] private Animator anim = default;

    protected override void OnEnter(PlayerMovement player)
    {
        playerPosMemory.value = playerPosition;
        StartCoroutine(StartSceneCo(player));
    }

    private IEnumerator StartSceneCo(PlayerMovement player)
    {
        player.currentState = new Locked(player, transitionTime + 2f);
        anim.SetTrigger("StartLoading");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneToLoad);
    }
}
