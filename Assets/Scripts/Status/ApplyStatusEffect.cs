using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplyStatusEffect : IEffect
{
    private System.Type statusType;
    private int stack;

    public ApplyStatusEffect(System.Type type, int stack)
    {
        statusType = type;
        this.stack = stack;
    }

    public int GetValue() => 0;

    public EffectType GetEffectType() => EffectType.ApplyStatus;

    public void Resolve(EffectContext ctx)
    {
        var status = (Status)System.Activator.CreateInstance(statusType);
        ctx.target.statusManager.AddStatus(status, stack);
    }
}