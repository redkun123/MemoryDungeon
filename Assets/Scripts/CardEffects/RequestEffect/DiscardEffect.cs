using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/Card Effect/Discard")]
public class DiscardEffect : CardEffect
{
    [SerializeField] public int discardAmount;
    public override IEffectExecute CreateEffect(Character source, Character target)
    {
        return new DiscardExecute(discardAmount);
    }
}
