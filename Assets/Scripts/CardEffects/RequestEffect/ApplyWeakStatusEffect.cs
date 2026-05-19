using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/Card Effect/Weak")]
public class ApplyWeakStatusEffect : CardEffect
{
    [SerializeField] public int stack;
    public override IEffectExecute CreateEffect(Character source, Character target)
    {
        return new ApplyStatusExecute(typeof(WeakStatus), stack);
    }
}
