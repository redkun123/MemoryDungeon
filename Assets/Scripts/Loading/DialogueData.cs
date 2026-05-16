using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "Story/Dialogue")]
public class DialogueData : ScriptableObject
{
    [SerializeField] public List<DialogueLine> dialogueList;
}
