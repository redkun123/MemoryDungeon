using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class GameSaveData
{
    public List<string> completedRoomIds = new List<string>();

    public int killTotal;
    public int roomDiscoverTotal;
    public HashSet<string> ToHashSet()
    {
        return new HashSet<string>(completedRoomIds);
    }
    public void FromHashSet(HashSet<string> set)
    {
        completedRoomIds = new List<string>(set);
    }
}
