using UnityEngine;

public class NearbySound : MonoBehaviour
{
    public float detectionRadius = 3f; // Adjust as needed
    public float maxVolume = 0.5f; // Adjust the maximum volume
    public float minVolume = 0.1f; // Adjust the minimum volume
    public AnimationCurve volumeCurve; // You can adjust this curve in the Inspector
    [SerializeField] private Transform player;
    private AudioSource audioSource;
    private bool hasPlayedAnimationEventSound = false; // Flag to track if animation event sound has been played

    private void Start()
    {
        if(FindObjectOfType<PlayerMovement>())
        {
            Debug.Log("A Playerobject was found");
            player = FindObjectOfType<PlayerMovement>().transform; // Adjust this to find your player object.
        }
        if (FindObjectOfType<ShipMovement>())
        {
            Debug.Log("A Shipobject was found");
            player = FindObjectOfType<ShipMovement>().transform; // Adjust this to find your player object.
        }

        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Calculate the volume based on the curve and distance
        float volume = Mathf.Lerp(minVolume, maxVolume, volumeCurve.Evaluate(distanceToPlayer / detectionRadius));

        // Set the audio volume
        audioSource.volume = volume;

        if (distanceToPlayer <= detectionRadius && !audioSource.isPlaying)
        {
            // Play the smith sound
            PlaySmithSound();
        }
        else if (distanceToPlayer > detectionRadius && audioSource.isPlaying)
        {
            // Stop the sound if the player is too far away
            audioSource.Stop();
        }
    }

    public void PlaySmithSound()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    // Animation event function to call PlaySmithSound when needed in the animation
    public void PlaySoundWithAnimationEvent()
    {
        if (!hasPlayedAnimationEventSound)
        {
            PlaySmithSound();
            hasPlayedAnimationEventSound = true;
        }
    }
}