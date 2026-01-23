using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Room/Room Database")]
public class RoomDB : ScriptableObject
{
    [Header ("Special Rooms")]
    [SerializeField] public Room startRoom;
    [SerializeField] public Room bossRoom;
    [SerializeField] public Room finalbossRoom;
    [SerializeField] public Room restRoom;
    [SerializeField] public Room shopRoom;

    [Header("Normal Rooms")]
    [SerializeField] public List<Room> normalRoom;
}
