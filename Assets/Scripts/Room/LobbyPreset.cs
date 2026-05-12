using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Room/Lobby Preset")]
public class LobbyPreset : ScriptableObject
{
    [SerializeField] public List<RoomPreset> listPreset;
    public Dictionary<string, RoomPreset> listRoomPreset;

    public void Init()
    {
        listRoomPreset = new();
        for (int i = 0; i < listPreset.Count; i++)
        {
            listRoomPreset.Add(listPreset[i].roomName, listPreset[i]);
        }
    }
    public RoomPreset _lookup(string key)
    {
        if (listRoomPreset.TryGetValue(key, out var roomPreset))
        {
            return roomPreset;
        }
        Debug.Log("Room preset not found");
        return null;
    }
}
