using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardExecute : IEffectExecute
{
    private int guard;

    public GuardExecute(int guard)
    {
        this.guard = guard;
    }

    public int GetValue() => guard;

    public EffectType GetEffectType() => EffectType.Block;

    public void Resolve(EffectContext ctx)
    {
        ctx.target.GainGuard(ctx.value);
    }
}
