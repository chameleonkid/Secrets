using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class SzeneTransition : MonoBehaviour
{
    [Header("New Scene Variables")]
    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerPosMemory;
    [SerializeField] private float transitionTime = 3f;
    [SerializeField] private Animator anim = default;
    private PlayerMovement player;


    public void Awake()
    {

    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            player = other.GetComponent<PlayerMovement>();
            playerPosMemory.value = playerPosition;
            StartCoroutine(StartSceneCo());
        }
    }

    IEnumerator StartSceneCo()
    {
        player.GetComponent<PlayerMovement>().LockMovement(transitionTime + 2f);
        anim.SetTrigger("StartLoading");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneToLoad);
    }

}
