using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloorManager
{
    //[SerializeField] RoomManager roomManager;
    public RoomManager roomManager;
    private Room currentRoom;
    public int floor = 0;
    public void Init(RoomManager roomManager)
    {
        this.roomManager = roomManager;
        RoomOption();
    }
    public void RoomOption()
    {
        Debug.Log($"Current Floor: {floor}");
        if (floor < 0 || floor > 10)
        {
            Debug.Log("Floor count invalid");
        }
        switch (floor)
        {
            case 0:
                currentRoom = roomManager.ShowStartRoom();
                Debug.Log("Start room located");
                break;
            case 8:
                currentRoom = roomManager.ShowRestRoom();
                break;
            case 9:
                currentRoom = roomManager.ShowBossRoom();
                break;
            case 10:
                currentRoom = roomManager.ShowFinalBossRoom();
                Debug.Log("Final room located");
                break;
            default:
                //currentRoom = roomManager.ShowRandomRoom();
                break;
        }
        floor++;
        LoadCurrentRoom(currentRoom);
    }
    public void LoadCurrentRoom(Room chosenRoom)
    {
        SceneManager.LoadScene("BattleScene");

    }
}
