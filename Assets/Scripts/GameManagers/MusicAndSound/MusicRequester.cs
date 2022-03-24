using UnityEngine;

public class MusicRequester : MonoBehaviour
{
    [SerializeField] private AudioClip[] areaMusic = default;

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerMovement>();
        var ship = other.GetComponent<ShipMovement>();
        if (player != null || ship != null)
        {
            // MusicManager.RequestMusic(areaMusic.GetRandomElement());
            MusicManager.RequestMusic(areaMusic);           // Return Array
        }
    }
}
