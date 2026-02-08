using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName = "Story/Story Option")]

public class StoryOption : ScriptableObject
{
    [TextArea]
    public string optionDescription;
    public List<StoryResult> results;
}
