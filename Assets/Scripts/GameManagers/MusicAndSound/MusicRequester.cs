using UnityEngine;

public class MusicRequester : MonoBehaviour
{
    [SerializeField] private AudioClip[] areaMusic = default;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerMovement>();
        if (player != null)
        {
            MusicManager.RequestMusic(GetMusic());
        }
    }


    private AudioClip GetMusic() => areaMusic[Random.Range(0, areaMusic.Length - 1 )];
}
