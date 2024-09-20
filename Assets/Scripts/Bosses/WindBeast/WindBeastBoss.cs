using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBeastBoss : TurretEnemy
{
    [Header("Attack Cooldowns")]
    public float lineAttackCooldown = 5f;   // Cooldown duration for line attack
    public float circleAttackCooldown = 7f; // Cooldown duration for circle attack

    private float lineAttackTimer = 0f;     // Timer for line attack cooldown
    private float circleAttackTimer = 0f;   // Timer for circle attack cooldown

    [Header("Line Lightning Attack")]
    public GameObject lineAttackPrefab;
    public Transform lineStartPointLeft;
    public Transform lineEndPointLeft;
    public Transform lineStartPointRight;
    public Transform lineEndPointRight;
    public int lineIndicatorCount = 5;       // Number of attacks spawned on each side
    public float lineMoveDuration = 2f;      // Time for attacks to move toward the center

    [Header("Circle Lightning Attack")]
    public GameObject circleAttackPrefab;
    public int circleIndicatorCount = 20;
    public float circleRadius = 5f;
    public float circleIndicatorDuration = 1f;
    public float circleGapAngleSize = 30f;
    public float circleGapRotationSpeed = 30f;
    public bool circleGapCloses = false;

    [Header("Boss Attack Sounds")]
    [SerializeField] private AudioClip[] attackSound;
    [SerializeField] private AudioClip earthQuakeSound;

    [Header("Starting Dialog")]
    [SerializeField] private Dialogue dialogue = default;

    private Transform playerTransform;
    private Animator animator;

    protected override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();

        // Find the player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogError("Player not found in the scene.");
        }
    }

    void Update()
    {
        // Update cooldown timers
        if (lineAttackTimer > 0)
        {
            lineAttackTimer -= Time.deltaTime;
        }

        if (circleAttackTimer > 0)
        {
            circleAttackTimer -= Time.deltaTime;
        }

        // Decide when to attack
        DecideNextAttack();
    }

    private void DecideNextAttack()
    {
        if (lineAttackTimer <= 0 || circleAttackTimer <= 0)
        {
            int attackChoice = Random.Range(0, 2); // Randomly choose an attack: 0 for line, 1 for circle

            if (attackChoice == 0 && lineAttackTimer <= 0)
            {
                animator.SetTrigger("LineAttack");
                lineAttackTimer = lineAttackCooldown;
            }
            else if (attackChoice == 1 && circleAttackTimer <= 0)
            {
                animator.SetTrigger("CircleAttack");
                circleAttackTimer = circleAttackCooldown;
            }
        }
    }

    // This method is called from the animation event in the LineAttack animation
    public void ExecuteLineAttack()
    {
        StartCoroutine(LineAttackCoroutine());
        PlayAttackSound();
    }

    private IEnumerator LineAttackCoroutine()
    {
        // Create two sets of attack objects, one from left and one from right
        Vector2 directionLeftToRight = ((Vector2)lineEndPointLeft.position - (Vector2)lineStartPointLeft.position).normalized;
        Vector2 directionRightToLeft = ((Vector2)lineEndPointRight.position - (Vector2)lineStartPointRight.position).normalized;

        float distanceLeft = Vector2.Distance(lineStartPointLeft.position, lineEndPointLeft.position);
        float distanceRight = Vector2.Distance(lineStartPointRight.position, lineEndPointRight.position);

        float stepLeft = distanceLeft / (lineIndicatorCount - 1);
        float stepRight = distanceRight / (lineIndicatorCount - 1);

        // Arrays for attack objects on both sides
        GameObject[] attacksLeft = new GameObject[lineIndicatorCount];
        GameObject[] attacksRight = new GameObject[lineIndicatorCount];

        // Instantiate attack objects on both sides
        for (int i = 0; i < lineIndicatorCount; i++)
        {
            Vector2 positionLeft = (Vector2)lineStartPointLeft.position + directionLeftToRight * stepLeft * i;
            attacksLeft[i] = Instantiate(lineAttackPrefab, positionLeft, Quaternion.identity);

            Vector2 positionRight = (Vector2)lineStartPointRight.position + directionRightToLeft * stepRight * i;
            attacksRight[i] = Instantiate(lineAttackPrefab, positionRight, Quaternion.identity);
        }

        float elapsedTime = 0f;

        // Move the attacks towards each other over time
        while (elapsedTime < lineMoveDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / lineMoveDuration;

            for (int i = 0; i < lineIndicatorCount; i++)
            {
                if (attacksLeft[i] != null && attacksRight[i] != null)
                {
                    // Move attacks from the left side towards the center
                    Vector2 startPositionLeft = (Vector2)lineStartPointLeft.position + directionLeftToRight * stepLeft * i;
                    Vector2 endPositionLeft = (Vector2)lineEndPointLeft.position;
                    attacksLeft[i].transform.position = Vector2.Lerp(startPositionLeft, endPositionLeft, t);

                    // Move attacks from the right side towards the center
                    Vector2 startPositionRight = (Vector2)lineStartPointRight.position + directionRightToLeft * stepRight * i;
                    Vector2 endPositionRight = (Vector2)lineEndPointRight.position;
                    attacksRight[i].transform.position = Vector2.Lerp(startPositionRight, endPositionRight, t);
                }
            }

            yield return null;
        }

        // After the attacks reach the center, handle destruction or any special effects
        for (int i = 0; i < lineIndicatorCount; i++)
        {
            if (attacksLeft[i] != null)
            {
                Destroy(attacksLeft[i]);
            }

            if (attacksRight[i] != null)
            {
                Destroy(attacksRight[i]);
            }
        }
    }

    // This method is called from the animation event in the CircleAttack animation
    public void ExecuteCircleAttack()
    {
        StartCoroutine(CircleAttackCoroutine());
        PlayAttackSound();
    }

    private IEnumerator CircleAttackCoroutine()
    {
        float elapsedTime = 0f;
        float currentGapAngle = 0f;
        float initialGapSize = circleGapAngleSize;
        List<GameObject> indicators = new List<GameObject>();

        while (elapsedTime < circleIndicatorDuration)
        {
            // Clear previous indicators
            foreach (var indicator in indicators)
            {
                Destroy(indicator);
            }
            indicators.Clear();

            // Update gap angle
            currentGapAngle += circleGapRotationSpeed * Time.deltaTime;
            if (currentGapAngle >= 360f)
                currentGapAngle -= 360f;

            // Optionally, close the gap over time
            float currentGapSize = circleGapCloses ? Mathf.Lerp(initialGapSize, 0f, elapsedTime / circleIndicatorDuration) : circleGapAngleSize;

            // Spawn indicators
            for (int i = 0; i < circleIndicatorCount; i++)
            {
                float angleStep = 360f / circleIndicatorCount;
                float angle = angleStep * i;

                // Check if the angle is within the gap
                float gapStart = currentGapAngle - currentGapSize / 2f;
                float gapEnd = currentGapAngle + currentGapSize / 2f;

                float normalizedAngle = (angle + 360f) % 360f;
                float normalizedGapStart = (gapStart + 360f) % 360f;
                float normalizedGapEnd = (gapEnd + 360f) % 360f;

                bool isInGap = IsAngleBetween(normalizedAngle, normalizedGapStart, normalizedGapEnd);

                if (isInGap)
                    continue;

                // Calculate position around the player
                float radAngle = Mathf.Deg2Rad * angle;
                Vector2 playerPosition = playerTransform.position;
                Vector2 offset = new Vector2(Mathf.Cos(radAngle), Mathf.Sin(radAngle)) * circleRadius;
                Vector2 position = playerPosition + offset;

                // Spawn indicator
                GameObject indicator = Instantiate(circleAttackPrefab, position, Quaternion.identity);
                indicators.Add(indicator);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Replace indicators with attacks
        foreach (var indicator in indicators)
        {
            Vector2 position = indicator.transform.position;
            Destroy(indicator);
            Instantiate(circleAttackPrefab, position, Quaternion.identity);
        }
        indicators.Clear();
    }

    private bool IsAngleBetween(float angle, float startAngle, float endAngle)
    {
        if (startAngle <= endAngle)
            return angle >= startAngle && angle <= endAngle;
        else
            return angle >= startAngle || angle <= endAngle;
    }

    private void PlayAttackSound()
    {
        if (attackSound.Length > 0)
        {
            AudioClip clip = attackSound[Random.Range(0, attackSound.Length)];
            SoundManager.RequestSound(clip);
        }
    }

    protected override void DeathEffect()
    {
        if (deathEffect != null)
        {
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            // Destroy(effect, deathEffectDelay); // Uncomment if you want to destroy the effect after a delay
        }
    }

    private void RequestDialogue()
    {
        dialogue.lines[0].text = "WHO IS DISTURBING WIND BEAST! WHAT??? YOU? I WILL ZAP YOU!";
        DialogueManager.RequestDialogue(dialogue);
    }
}