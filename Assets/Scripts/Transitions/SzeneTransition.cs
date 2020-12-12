using UnityEngine;
using UnityEngine.SceneManagement;

public class SzeneTransition : MonoBehaviour
{
    [Header("New Scene Variables")]
    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerPosMemory;

    [Header("Transition Variables")]
    public GameObject fadeInPanel;

    public void Awake()
    {
        if (fadeInPanel != null)
        {
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 1);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            playerPosMemory.value = playerPosition;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
