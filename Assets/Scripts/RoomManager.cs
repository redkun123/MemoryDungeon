using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager
{
    public List<int> usedRoomID = new();
    public List<Room> randRoom = new();
    public RoomDB roomDB;
    public Room currentRoom;
    public RoomManager(RoomDB roomDB)
    {
        this.roomDB = roomDB;
        Debug.Log("Room Manager created");
    }
    public void RoomComplete()
    {
        ClearChosenRoom(currentRoom);
        ShowLobby();
    }
    public void ShowLobby()
    {
        SceneManager.LoadScene("LobbyScene");
    }
    public Room ShowStartRoom()
    {
        Debug.Log("ShowStartRoom CALLED");
        currentRoom = roomDB.startRoom;
        Debug.Log("Start Room located");
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
    public Room ShowRandomRoom()
    {
        currentRoom = randRoom[RunManager.Instance.temp];
        ClearTempList();
        return currentRoom;
    }
    public void SpawnRandomRoom()
    {
        Debug.Log("Spawning room");
        List<Room> roomPool = new();
        roomPool.AddRange(roomDB.normalRoom);
        if (usedRoomID != null)
        {
            roomPool.RemoveAll(room => usedRoomID.Contains(room.roomID));
            Debug.Log("Used rooms removed");
        }
        roomPool.Add(roomDB.restRoom);
        roomPool.Add(roomDB.shopRoom);
        Debug.Log("Room pool created");
        Extensions.Shuffle(roomPool);
        Debug.Log($"Số phòng trong pool: {roomPool.Count}");
        if (roomPool.Count < 3)
        {
            Debug.LogError("Not enough rooms in pool");
            return;
        }
        for (int j = roomPool.Count - 1; j > 2; j--)
        {
            roomPool.RemoveAt(j);
        }
        Debug.Log($"Số phòng trong room pool còn: {roomPool.Count}");
        randRoom.AddRange(roomPool);
        Debug.Log("Room pool finished");
    }
    public void ClearChosenRoom(Room chosenRoom)
    {
        if (chosenRoom.roomType == Room.RoomType.Story || chosenRoom.roomType == Room.RoomType.Battle)
        {
            usedRoomID.Add(chosenRoom.roomID);
        }
        else return;
    }
    public void ClearTempList()
    {
        randRoom.Clear();
    }
    public void EnterChosenRoom(Room chosenRoom)
    {
        Debug.Log("Trying to enter chosen room");
        string sceneName = SceneMap.GetScene(chosenRoom.roomType);
        SceneManager.LoadScene(sceneName);
        switch (chosenRoom.roomType)
        {
            case Room.RoomType.Battle:
                RoomBattle roomBattle = (RoomBattle)chosenRoom;
                LoadBattleRoom(roomBattle);
                break;
            case Room.RoomType.Story:
                RoomStory roomStory = (RoomStory)chosenRoom;
                LoadStoryRoom(roomStory);
                break;
            case Room.RoomType.Shop:
                RoomShop roomShop = (RoomShop)chosenRoom;
                LoadShopRoom(roomShop);
                break;
            case Room.RoomType.Rest:
                RoomRest roomRest = (RoomRest)chosenRoom;
                LoadRestRoom(roomRest);
                break;
        }
    }
    public void LoadBattleRoom(RoomBattle chosenRoom)
    {
        Debug.Log("Loading battle");
        RunManager.Instance.StartBattle(chosenRoom.enemyConfig);
    }
    public void LoadShopRoom(RoomShop chosenRoom)
    {
        Debug.Log("Loading shop");
    }
    public void LoadRestRoom(RoomRest chosenRoom)
    {
        Debug.Log("Loading hotel");
    }
    public void LoadStoryRoom(RoomStory chosenRoom)
    {
        Debug.Log("Loading story");
    }
}
