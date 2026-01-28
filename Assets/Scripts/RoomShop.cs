using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Room/Shop Room")]
public class RoomShop : Room
{
    public Player player;
    public override RoomType roomType => RoomType.Shop;
    public void BuyItem(Player player, int cost)
    {
        Extensions.PayGold(player.gold, cost);
    }
    public void RoomOption()
    {
        //BuyItem(player);
    }
}
