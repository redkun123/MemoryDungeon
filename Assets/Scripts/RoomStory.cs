using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Room/Story Room")]
public class RoomStory : Room
{
    [SerializeField] Enemy enemy;
    public RoomType roomType = RoomType.Story;
}
