using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/Card Effect/Guard")]
public class GuardEffect : CardEffect
{
    [SerializeField] public int guard;
    public override IEffectExecute CreateEffect(Character source, Character target)
    {
        return new GuardExecute(guard);
    }
}
