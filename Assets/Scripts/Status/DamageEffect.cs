using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEffect : IEffect
{
    private int damage;

    public DamageEffect(int damage)
    {
        this.damage = damage;
    }

    public int GetValue() => damage;

    public EffectType GetEffectType() => EffectType.Damage;

    public void Resolve(EffectContext ctx)
    {
        ctx.target.TakeDamage(ctx.value);
    }
}
