using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Story/Story Result")]


public class StoryResult : ScriptableObject
{
    public ResultType type;
    public string value;
    public enum ResultType
    {
        None,
        InitNode,
        ModifyGold,
        ModifyHP,
        ModifyCard,
        ModifyRelic,
        Leave
    }
}
