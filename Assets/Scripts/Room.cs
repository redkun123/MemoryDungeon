using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : ScriptableObject
{
    [SerializeField] public int roomID;
    [SerializeField] public string roomName;
    public RoomType roomType;
    public 
    public enum RoomType
    {
        Rest,
        Shop,
        Story,
        Battle,
        Boss
    }
}
