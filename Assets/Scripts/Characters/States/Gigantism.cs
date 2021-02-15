using UnityEngine;

public class Gigantism : Status
{
    [SerializeField] private float scaleMultiplier = 1.5f;
    [SerializeField] private float duration = 2;
    [SerializeField] private int maxStacks = 3;

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

    private IGigantism target;

    public void Initialise(IGigantism target)
    {
        this.target = target;
        timers = new float[maxStacks];
    }

    public override void Trigger()
    {
        if (target == null) return;

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

            target.transform.localScale *= scaleMultiplier;
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
            target.transform.localScale /= scaleMultiplier;
        }

        for (int i = 0; i < timers.Length; i++)
        {
            timers[i] = 0;
        }

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
                    target.transform.localScale /= scaleMultiplier;
                }

                if (stacks <= 0)
                {
                    this.enabled = false;
                }
            }
        }
    }
}

public interface IGigantism
{
    Transform transform { get; }
    Gigantism gigantism { get; }
}
