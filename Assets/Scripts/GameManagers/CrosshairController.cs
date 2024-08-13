using UnityEngine;

public class CrosshairController : MonoBehaviour
{
    public float maxDistanceFromPlayer = 3f; // Maximum distance the crosshair can move from the player
    private Transform player;
    private SpriteRenderer crosshairSpriteRenderer;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform; // Ensure the player has the "Player" tag
        crosshairSpriteRenderer = GetComponent<SpriteRenderer>();
        Cursor.visible = false; // Hide the default cursor
    }

    void Update()
    {
        // Get input from the right stick
        float rightStickHorizontal = Input.GetAxis("RightStickHorizontal");
        float rightStickVertical = Input.GetAxis("RightStickVertical");

        // Create an input vector from the right stick axes
        Vector2 input = new Vector2(rightStickHorizontal, rightStickVertical);

        // Check if there's any significant input from the right stick
        if (input.magnitude > 0.1f)
        {
            // Show the crosshair
            crosshairSpriteRenderer.enabled = true;

            // Calculate the new position of the crosshair
            Vector2 targetPosition = (Vector2)player.position + input.normalized * maxDistanceFromPlayer;

            // Clamp the crosshair within the maximum distance from the player
            Vector2 directionFromPlayer = targetPosition - (Vector2)player.position;
            float distance = Mathf.Clamp(directionFromPlayer.magnitude, 0, maxDistanceFromPlayer);

            // Set the position of the crosshair
            transform.position = (Vector2)player.position + directionFromPlayer.normalized * distance;
        }
        else
        {
            // Hide the crosshair when there's no input
            crosshairSpriteRenderer.enabled = false;

            // Optionally reset the crosshair to the player's position
            transform.position = player.position;
        }
    }
}