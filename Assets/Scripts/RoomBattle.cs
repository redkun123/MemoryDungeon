using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Room/Battle Room")]
public class RoomBattle : Room
{
    [SerializeField] public Enemy enemy;
    public RoomType roomType = RoomType.Battle;
}
