using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/Card Effect/Attack")]
public class AttackEffect : CardEffect
{
    [SerializeField] public int damage;
    public override void Execute(Character targetChar)
    {
        targetChar.TakeDamage(damage);
        Debug.Log($"{targetChar} took {damage} dmg");
    }
    public override void Execute(List<Card> targetCard)
    {

    }
}
