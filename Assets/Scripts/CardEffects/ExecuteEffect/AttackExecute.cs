using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackExecute : IEffectExecute
{
    private int damage;

    public AttackExecute(int damage)
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
