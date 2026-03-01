using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/Card Effect/Heal")]
public class HealEffect : CardEffect
{
    [SerializeField] public int healAmount;
    public override void Execute(Character targetChar)
    {
        targetChar.RestoreHP(healAmount);
        Debug.Log($"{targetChar} restored {healAmount} HP");
    }
    public override void Execute(List<Card> targetCard)
    {

    }
    public override void Execute()
    {

    }
}
