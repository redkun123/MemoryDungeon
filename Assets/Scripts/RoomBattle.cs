using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Room/Battle Room")]
public class RoomBattle : Room
{
    [SerializeField] public EnemyConfig enemyConfig;
    public override RoomType roomType => RoomType.Battle;
}
