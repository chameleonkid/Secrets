using UnityEngine;

public class Slow : Status
{
    [SerializeField] private float speedMultiplier = 0.5f;
    [SerializeField] private Color colorChange = new Color(0.03f, 0.25f, 0.0f, 1);
    [SerializeField] private float duration = 3;
    [SerializeField] private int maxStacks = 8;
    [SerializeField] private GameObject particleLight = default;

    [SerializeField] private ParticleSystem particles = default;

    /// <summary>
    /// This value should be cached where possible (uses a for loop).
    /// </summary>
    public int stacks
    {
        get
        {
            int s = 0;
            for (int i = 0; i < timers.Length; i++)
            {
                if (timers[i] > 0)
                {
                    s++;
                }
            }
            return s;
        }
    }

    private float[] timers;

    private Color initialColor;

    private ISlow target;

    public void Initialise(ISlow target)
    {
        this.target = target;
        timers = new float[maxStacks];

        initialColor = target.renderer.color;
    }

    public override void Trigger()
    {
        if (target == null) return;

        particleLight.SetActive(true);
        var e = particles.emission;
        e.enabled = true;

        if (stacks < maxStacks)
        {
            for (int i = 0; i < timers.Length; i++)
            {
                if (timers[i] <= 0)
                {
                    timers[i] = duration;
                    break;
                }
            }

            target.speedModifier *= speedMultiplier;

            if (target.renderer.color != colorChange)
            {
                target.renderer.color = colorChange;
            }
        }

        if (stacks > 0)
        {
            this.enabled = true;
        }
    }

    public override void Clear()
    {
        if (target == null) return;



        for (int i = 0; i < stacks; i++)
        {
            target.speedModifier /= speedMultiplier;
        }

        for (int i = 0; i < timers.Length; i++)
        {
            timers[i] = 0;
        }

        target.renderer.color = initialColor;

        var e = particles.emission;
        e.enabled = false;
        particleLight.SetActive(false);

        this.enabled = false;
    }

    protected override void Update()
    {
        if (target == null) return;

        for (int i = 0; i < timers.Length; i++)
        {
            if (timers[i] > 0)
            {
                timers[i] -= Time.deltaTime;

                if (timers[i] < 0)
                {
                    target.speedModifier /= speedMultiplier;
                }

                if (stacks <= 0)
                {
                    target.renderer.color = initialColor;
                    var e = particles.emission;
                    e.enabled = false;
                    particleLight.SetActive(false);
                    this.enabled = false;
                }
            }
        }
    }
}

public interface ISlow
{
    Slow slow { get; }
    float speedModifier { get; set; }
    SpriteRenderer renderer { get; }
}
