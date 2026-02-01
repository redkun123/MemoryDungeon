using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Room/Story Room")]
public class RoomStory : Room
{
    public override RoomType roomType => RoomType.Story;
}
