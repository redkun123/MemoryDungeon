using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VulnerableStatus : Status
{
    public override void Modify(EffectContext ctx)
    {
        name = "Vulnerable";
        if (ctx.type == EffectType.Damage && ctx.source == owner)
        {
            ctx.value = Mathf.RoundToInt(ctx.value * 1.5f);
        }
    }
    public override void OnTurnEnd()
    {
        stack -= 1;
    }
}