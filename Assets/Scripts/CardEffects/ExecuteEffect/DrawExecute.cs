using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawExecute : IEffectExecute
{
    private int drawAmount;

    public DrawExecute(int cardAmount)
    {
        this.drawAmount = cardAmount;
    }

    public int GetValue() => drawAmount;

    public EffectType GetEffectType() => EffectType.Draw;

    public void Resolve(EffectContext ctx)
    {
        var player = (Player)ctx.source;
        for (int i = 0; i < drawAmount; i++)
        {
            player.DrawOne();
        }
    }
}
