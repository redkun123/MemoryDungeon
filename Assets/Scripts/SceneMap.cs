using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Room;

public static class SceneMap
{
    public static string GetScene(RoomType type)
    {
        return type switch
        {
            RoomType.Battle => "BattleScene",
            RoomType.Story => "EventScene",
            RoomType.Shop => "ShopScene",
            RoomType.Boss => "BossScene",
            _ => throw new Exception("Unknown room type")
        };
    }
}
