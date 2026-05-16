using System;
using UnityEngine;

[Serializable]
public struct DialogueLine
{
    public string speaker;

    [TextArea(3, 6)]
    public string dialogue;
}