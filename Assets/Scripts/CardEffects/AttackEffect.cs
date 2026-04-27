using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/Card Effect/Attack")]
public class AttackEffect : CardEffect
{
    [SerializeField] public int damage;
    public override IEffect CreateEffect(Character source, Character target)
    {
        return new DamageEffect(damage);
    }
}
