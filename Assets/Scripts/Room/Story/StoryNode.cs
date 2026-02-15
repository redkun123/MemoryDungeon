using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Story/Story Node")]
public class StoryNode: ScriptableObject
{
    public string nodeName;
    public string nodeID;
    public Sprite nodeImage;
    public string nodeDescription;
    public List<StoryButton> optionButton;
}
