using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager
{
    [SerializeField] RoomManager roomManager;
    private Room currentRoom;
    public int floor = 0;
    public void RoomOption()
    {
        if (floor < 0 || floor > 10)
        {
            Debug.Log("Floor count invalid");
        }
        switch (floor)
        {
            case 0:
                roomManager.ShowStartRoom();
                break;
            case 8:
                roomManager.ShowRestRoom();
                break;
            case 9:
                roomManager.ShowBossRoom();
                break;
            case 10:
                roomManager.ShowFinalBossRoom();
                break;
            default:
                roomManager.ShowRandomRoom();
                break;
        }
        floor++;
    }
    public void LoadCurrentRoom(Room chosenRoom)
    {

    }
}
