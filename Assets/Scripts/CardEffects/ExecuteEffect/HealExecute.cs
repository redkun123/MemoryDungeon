using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealExecute : IEffectExecute
{
    private int healAmount;

    public HealExecute(int healAmount)
    {
        this.healAmount = healAmount;
    }

    public int GetValue() => healAmount;

    public EffectType GetEffectType() => EffectType.Heal;

    public void Resolve(EffectContext ctx)
    {
        ctx.target.RestoreHP(ctx.value);
    }
}
