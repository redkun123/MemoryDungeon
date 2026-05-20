using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomManager
{
    public List<string> usedRoomID = new();
    public List<Room> randRoom = new();
    public RoomDB roomDB;
    public Room currentRoom;
    private int selectedRoomIndex;
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
        currentRoom = roomDB.finalBossRoom;
        return currentRoom;
    }
    public void ShowPrologue()
    {
        RunManager.Instance.LoadPrologue();
    }
    public void SetSelectedRoom(int index)
    {
        selectedRoomIndex = index;
    }
    public Room ShowRandomRoom()
    {
        currentRoom = randRoom[selectedRoomIndex];
        return currentRoom;
    }
    public void SpawnRandomRoom()
    {
        if (randRoom != null)
        {
            randRoom.Clear();
        }
        Debug.Log("Spawning room");
        List<Room> roomPool = new();
        roomPool.AddRange(roomDB.normalRoom);
        if (usedRoomID != null)
        {
            roomPool.RemoveAll(room => usedRoomID.Contains(room.roomID));
            Debug.Log("Used rooms removed");
        }
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
    public void RemoveSpecialRoom()
    {
        string restRoomID = "3";
        string shopRoomID = "4";
        usedRoomID.Add(restRoomID);
        usedRoomID.Add(shopRoomID);
    }
    public void AddSpecialRoom()
    {
        string restRoomID = "3";
        string shopRoomID = "4";
        usedRoomID.Remove(restRoomID);
        usedRoomID.Remove(shopRoomID);
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
        if (chosenRoom == null)
        {
            Debug.LogError("ChosenRoom is null");
            return;
        }
        Debug.Log("Trying to enter chosen room");
        string sceneName = SceneMap.GetScene(chosenRoom.roomType);
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError($"No scene for room type: {chosenRoom.roomType}");
            return;
        }
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
        RunManager.Instance.UpdateRunSave();
        Debug.Log("Auto saved");
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
        RunManager.Instance.StartStory(chosenRoom);
        Debug.Log("Loading story");
    }
}
