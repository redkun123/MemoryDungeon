using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Room/Room Database")]
public class RoomDB : ScriptableObject
{
    [Header("Special Rooms")]
    [SerializeField] public Room startRoom;
    [SerializeField] public Room shopRoom;
    [SerializeField] public Room restRoom;
    [SerializeField] public Room bossRoom;
    [SerializeField] public Room finalBossRoom;

    [Header("Normal Rooms")]
    [SerializeField] public List<Room> normalRoom;
    public List<Room> roomDatabase;
    public Dictionary<string, Room> _lookup;

    public void Init()
    {
        roomDatabase.Clear();
        for (int i = 0; i < normalRoom.Count; i++)
        {
            roomDatabase.Add(normalRoom[i]);
        }
        roomDatabase.Add(startRoom);
        roomDatabase.Add(shopRoom);
        roomDatabase.Add(restRoom);
        roomDatabase.Add(bossRoom);
        roomDatabase.Add(finalBossRoom);
        foreach (var room in roomDatabase)
        {
            Debug.Log($"ID: {room.roomID} - Room Name: {room.name}");
        }
        if (_lookup != null)
        {
            _lookup.Clear();
        }
        _lookup = new Dictionary<string, Room>();
        foreach (var room in roomDatabase)
        {
            if (_lookup.ContainsKey(room.roomID))
            {
                Debug.LogError($"Duplicate room id: {room.roomID}");
                continue;
            }
            _lookup.Add(room.roomID, room);
        }
    }
    public Room GetRoom(string id)
    {
        Init();
        Debug.Log($"Looking for: {id}");
        foreach (var key in _lookup.Keys)
        {
            Debug.Log($"DB has: {key}");
        }
        if (_lookup.TryGetValue(id, out var room)) return room;
        roomDatabase.Clear();
        Debug.LogError($"Room not found: {id}");
        return null;
    }
}
