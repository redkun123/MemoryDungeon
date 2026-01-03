using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/Card Effect/Attack")]
public class AttackEffect : CardEffect
{
    [SerializeField] public int damage;
    public override void Execute(CardContext ctx)
    {
        ctx.target.TakeDamage(damage);
    }
}
