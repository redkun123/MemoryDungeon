using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeakStatus : Status
{
    public override void GetName()
    {
        name = "Weak";
    }
    public override void Modify(EffectContext ctx)
    {
        if (ctx.type == EffectType.Damage && ctx.source == owner)
        {
            ctx.value = Mathf.RoundToInt(ctx.value * 0.75f);
        }
    }
    public override void OnTurnEnd()
    {
        stack -= 1;
    }
}
