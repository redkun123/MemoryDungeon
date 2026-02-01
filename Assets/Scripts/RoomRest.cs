using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

[CreateAssetMenu(menuName = "Room/Rest Room")]
public class RoomRest : Room
{
    [SerializeField] public int healCost;
    [SerializeField] public double healAmount;
    public Action<RoomRest> RoomCompleted;
    public override RoomType roomType => RoomType.Rest;
}
