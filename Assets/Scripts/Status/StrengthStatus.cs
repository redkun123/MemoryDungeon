using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthStatus : Status
{
    public override void Modify(EffectContext ctx)
    {
        name = "Strength";
        if (ctx.type == EffectType.Damage && ctx.source == owner)
        {
            ctx.value += stack;
        }
    }
}