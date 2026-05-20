using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VulnerableStatus : Status
{
    public override void GetName()
    {
        name = "Vulnerable";
    }
    public override void Modify(EffectContext ctx)
    {
        if (ctx.type == EffectType.Damage && ctx.target == owner)
        {
            ctx.value = Mathf.RoundToInt(ctx.value * 1.5f);
        }
    }
    public override void OnTurnStart()
    {
        stack -= 1;
    }
}