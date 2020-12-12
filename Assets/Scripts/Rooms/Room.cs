using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] protected GameObject virtualCamera = default;
    [SerializeField] protected Enemy[] enemies = default;
    [SerializeField] protected Breakable[] breakables = default;

    public void OnDisable() => virtualCamera.SetActive(false);

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            SetActiveRoom(true);
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            SetActiveRoom(false);
        }
    }

    protected void SetActiveRoom(bool value)
    {
        virtualCamera.SetActive(value);

        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].gameObject.SetActive(value);
        }

        for (int i = 0; i < breakables.Length; i++)
        {
            breakables[i].gameObject.SetActive(value);
        }
    }
}
