using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscardExecute : IEffectExecute
{
    private int cardAmount;

    public DiscardExecute(int cardAmount)
    {
        this.cardAmount = cardAmount;
    }

    public int GetValue() => cardAmount;

    public EffectType GetEffectType() => EffectType.Discard;

    public void Resolve(EffectContext ctx)
    {
        //ctx.target.(ctx.value);
        //for (int i = 0; i < discardAmount; i++)
        //{
        //    RunManager.Instance.player.Discard(targetCard[i]);
        //}
    }
}
