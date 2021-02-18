using UnityEngine;

public class Dashless : Status
{
    [SerializeField] private float duration = 4;
    [SerializeField] private ParticleSystem particles = default;

    private float timer;

    private IDashless target;

    public void Initialise(IDashless target) => this.target = target;

    public override void Trigger()
    {
        if (target == null) return;

        target.canDash = false;
        timer = duration;

        var e = particles.emission;
        e.enabled = true;

        this.enabled = true;
    }

    public override void Clear() => Clear(false);
    private void Clear(bool forceClear)
    {
        if (target == null) return;

        if (timer > 0 || forceClear)
        {
            target.canDash = true;
            timer = 0;
        }

        var e = particles.emission;
        e.enabled = false;

        this.enabled = false;
    }

    protected override void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            Clear(true);
        }
    }
}

public interface IDashless
{
    Dashless dashless { get; }
    bool canDash { get; set; }
}
