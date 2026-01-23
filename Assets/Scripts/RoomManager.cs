using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager
{
    [SerializeField] RunManager runManager;
    [SerializeField] RoomDB roomDB;

    public List<int> usedRoom = new();
    public event Action<Room> roomCompleted;
    Room currentRoom;
    public Room GetBattleRoom()
    {
        Room room = null;
        return room;
    }
    public Room ShowStartRoom()
    {
        currentRoom = roomDB.startRoom;
        return currentRoom;
    }
    public Room ShowRestRoom()
    {
        currentRoom = roomDB.restRoom;
        return currentRoom;
    }
    public Room ShowBossRoom()
    {
        currentRoom = roomDB.bossRoom;
        return currentRoom;
    }
    public Room ShowFinalBossRoom()
    {
        currentRoom = roomDB.finalbossRoom;
        return currentRoom;
    }
    public List<Room> ShowRandomRoom()
    {
        List<Room> roomPool = new();
        for (int i = 0; i < usedRoom.Count; i++)
        {
            roomDB.normalRoom.RemoveAt(usedRoom[i]);
        }
        roomPool.AddRange(roomDB.normalRoom);
        roomPool.Add(roomDB.restRoom);
        roomPool.Add(roomDB.shopRoom);
        //roomPool.RemoveRange(usedRoom);
        Extensions.Shuffle(roomPool);
        List<Room> temp = new();
        for (int i = 0; i <= 2; i++)
        {
            temp.Add(roomPool[i]);
        }
        return temp;
    }

    public void ClearChosenRoom(Room chosenRoom)
    {
        if (chosenRoom.roomType == Room.RoomType.Story || chosenRoom.roomType == Room.RoomType.Battle)
        {
            usedRoom.Add(chosenRoom.roomID);
        }
        else return;
    }
    public void EnterChosenRoom(Room chosenRoom)
    {
        string sceneName = SceneMap.GetScene(chosenRoom.roomType);
        SceneManager.LoadScene(sceneName);
        switch (chosenRoom.roomType)
        {
            case Room.RoomType.Battle:
                LoadBattleRoom(chosenRoom);
                break;
        }
    }
    public void LoadBattleRoom(RoomBattle chosenRoom)
    {
        runManager.StartBattle(chosenRoom.enemy);
    }
}
