using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/Card Effect/Draw")]
public class DrawEffect : CardEffect
{
    [SerializeField] public int drawAmount;
    public override void Execute(Character targetChar)
    {

    }
    public override void Execute(List<Card> targetCard)
    {

    }
    public override void Execute()
    {
        var player = RunManager.Instance.player;
        for (int i = 0; i < drawAmount; i++)
        {
            player.DrawOne();
        }
    }
}
