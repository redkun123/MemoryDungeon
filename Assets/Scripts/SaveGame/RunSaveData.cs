using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[System.Serializable]
public class RunSaveData
{
    public int currentHP;
    public int maxHP;
    public int gold;
    public int floor;
    public string currentRoomID;
    public List<string> deckCardIds;
    public List<string> visitedRoomIds;
    public List<string> relicList;
    public int killThisRun;
    public int roomDiscoverThisRun;
    //public int seed; // optional

    public HashSet<string> ToHashSet()
    {
        return new HashSet<string>(visitedRoomIds);
    }
}
