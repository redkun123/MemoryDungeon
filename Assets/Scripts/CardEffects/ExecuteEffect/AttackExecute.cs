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

    public IEnumerator Resolve(EffectContext ctx)
    {
        yield return ctx.source.Attack(ctx.value);
        ctx.target.TakeDamage(ctx.value);
        Debug.Log($"{ctx.source.name} attacked {ctx.target.name} for {ctx.value} damage!");
        yield return new WaitForSeconds(0.3f);
    }
}
