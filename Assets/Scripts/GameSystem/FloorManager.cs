using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloorManager
{
    public RoomManager roomManager;
    private Room currentRoom;
    public int floor;
    public void Init(RoomManager roomManager, int floor)
    {
        this.roomManager = roomManager;
        this.floor = floor;
    }
    public void DefineCurrentRoom()
    {
        Debug.Log($"Current Floor: {floor}");
        if (floor < 0 || floor > 11)
        {
            Debug.Log("Floor count invalid");
        }
        switch (floor)
        {
            default:
                currentRoom = roomManager.ShowRandomRoom();
                Debug.Log("Start room located");
                break;
        }
        floor++;
        AdjustSpecialRoom();
        RunManager.Instance.UpdateStatusBar();
        RunManager.Instance.currentRoom = currentRoom;
        roomManager.EnterChosenRoom(currentRoom);
    }
    public void AdjustSpecialRoom()
    {
        switch (floor)
        {
            case 1:
                roomManager.RemoveSpecialRoom();
                Debug.Log("Removed Shop and Rest");
                break;
            case 5:
                roomManager.AddSpecialRoom();
                Debug.Log("Added Shop and Rest");
                break;
            default:
                break;
        }
    }
}
