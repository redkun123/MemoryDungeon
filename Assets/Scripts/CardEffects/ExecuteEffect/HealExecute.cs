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

    public IEnumerator Resolve(EffectContext ctx)
    {
        ctx.target.RestoreHP(ctx.value);
        yield return null;
        yield return new WaitForSeconds(0.3f);
    }
}
