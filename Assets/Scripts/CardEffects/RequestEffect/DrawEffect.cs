using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/Card Effect/Draw")]
public class DrawEffect : CardEffect
{
    [SerializeField] public int drawAmount;
    public override IEffectExecute CreateEffect(Character source, Character target)
    {
        return new DrawExecute(drawAmount);
    }
}
