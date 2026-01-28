using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Room/Rest Room")]
public class RoomRest : Room
{
    public Player player;
    [SerializeField] int healCost;
    public override RoomType roomType => RoomType.Rest;
    public void Rest(Player player)
    {
        Extensions.PayGold(player.gold, healCost);
        int healHP = Convert.ToInt32(Math.Round((player.currentHP * 0.3)));
        player.RestoreHP(healHP);
    }
    public void RoomOption()
    {
        Rest(player);
    }
}
