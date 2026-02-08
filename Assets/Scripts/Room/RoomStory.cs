using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Room/Story Room")]
public class RoomStory : Room
{
    public override RoomType roomType => RoomType.Story;
    public string storyTitle;
    public string startingNodeID;
    public List<StoryNode> storyNodes;
}