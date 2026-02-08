using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class SaveData
{
    public int gold;
    public int currentHP;
    public int maxHP;
    public int currentRoomId;
    public List<int> completedRooms;
    public List<Card> currentDeck;
    public SaveData()
    {
        completedRooms = new List<int>();
        currentDeck = new List<Card>();
    }
}
