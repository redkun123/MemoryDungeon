using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/Card Effect/Heal")]
public class HealEffect : CardEffect
{
    [SerializeField] public int healAmount;
    public override IEffectExecute CreateEffect(Character source, Character target)
    {
        return new HealExecute(healAmount);
    }
}
