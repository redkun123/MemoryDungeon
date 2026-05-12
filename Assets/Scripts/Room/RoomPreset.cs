using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Room/Room Preset")]
public class RoomPreset : ScriptableObject
{
    [SerializeField] public string roomName;
    [SerializeField] public Sprite roomBG;
    [SerializeField] public Sprite roomIcon;
}
