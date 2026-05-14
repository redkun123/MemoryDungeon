using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrengthStatus : Status
{
    public override void GetName()
    {
        name = "Strength";
    }
    public override void Modify(EffectContext ctx)
    {
        if (ctx.type == EffectType.Damage && ctx.source == owner)
        {
            ctx.value += stack;
        }
    }
}