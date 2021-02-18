using UnityEngine;

public class Poison : Status
{
    [SerializeField] private float poisonDmg = 2;
    [SerializeField] private Color colorChange = new Color(0f, 0.7667949f, 0f, 1);
    [SerializeField] private float duration = 3;
    [SerializeField] private int maxStacks = 8;

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

    private IPoison target;

    public void Initialise(IPoison target)
    {
        this.target = target;
        timers = new float[maxStacks];

        initialColor = target.renderer.color;
    }

    public override void Trigger()
    {
        if (target == null) return;

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

            target.health -= poisonDmg;
            DamagePopUpManager.RequestDamagePopUp(poisonDmg, false, GetComponentInParent<Transform>());

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
            target.health -= poisonDmg;
            DamagePopUpManager.RequestDamagePopUp(poisonDmg, false, GetComponentInParent<Transform>());
        }

        for (int i = 0; i < timers.Length; i++)
        {
            timers[i] = 0;
        }

        target.renderer.color = initialColor;

        var e = particles.emission;
        e.enabled = false;

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
                    target.health -= poisonDmg;
                    DamagePopUpManager.RequestDamagePopUp(poisonDmg, false, GetComponentInParent<Transform>());
                }

                if (stacks <= 0)
                {
                    target.renderer.color = initialColor;
                    var e = particles.emission;
                    e.enabled = false;
                    this.enabled = false;
                }
            }
        }
    }
}

public interface IPoison
{
    Poison poison { get; }
    float health { get; set; }
    SpriteRenderer renderer { get; }
}
