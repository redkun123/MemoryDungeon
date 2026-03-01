using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/Card Effect/Discard")]
public class DiscardEffect : CardEffect
{
    [SerializeField] public int discardAmount;
    public override void Execute(Character targetChar)
    {

    }
    public override void Execute(List<Card> targetCard)
    {
        for (int i = 0; i < discardAmount; i++)
        {
            RunManager.Instance.player.Discard(targetCard[i]);
        }
    }
    public override void Execute()
    {

    }
}
