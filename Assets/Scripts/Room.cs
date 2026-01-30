using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : ScriptableObject
{
    [SerializeField] public int roomID;
    [SerializeField] public string roomName;
    public virtual RoomType roomType => RoomType.None;
    public enum RoomType
    {
        None,
        Rest,
        Shop,
        Story,
        Battle,
        Boss
    }
}
