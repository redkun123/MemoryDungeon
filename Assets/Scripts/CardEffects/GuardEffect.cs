using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/Card Effect/Guard")]
public class GuardEffect : CardEffect
{
    [SerializeField] public int guard;
    public override void Execute(Character targetChar)
    {
        targetChar.GainGuard(guard);
        Debug.Log($"{targetChar} gain {guard} guard");
    }
    public override void Execute(List<Card> targetCard)
    {

    }
}
