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

    public IEnumerator Resolve(EffectContext ctx)
    {
        var player = (Player)ctx.source;
        for (int i = 0; i < drawAmount; i++)
        {
            player.DrawOne();
            yield return new WaitForSeconds(0.3f);
        }
        yield return null;
    }
}
