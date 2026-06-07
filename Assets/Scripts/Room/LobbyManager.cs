using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] Transform optionParent;
    [SerializeField] LobbyOptionButton optionButtonPrefab;
    [SerializeField] private LobbyPreset lobbyPreset;
    private RoomManager roomManager;
    private List<GameObject> spawnedOptions;
    private List<int> optionID;
    private float spacing = 630f;
    private void Awake()
    {
        RunManager.Instance.RegisterLobbyManager(this);
        roomManager = RunManager.Instance.roomManager;
        optionID = new List<int>();
        spawnedOptions = new();
        var roomCount = RunManager.Instance.roomManager.randRoom.Count;
        lobbyPreset.Init();
        SpawnOptionButton(roomCount);
        UpdateOptionVisual();
    }
    private void OnDestroy()
    {
        spawnedOptions.Clear();
        optionID.Clear();
    }
    //Spawn nut option va gan ID option cho cac nut
    public void SpawnOptionButton(int buttonCount)
    {
        ClearOptions();
        for (int i = 0; i < buttonCount; i++)
        {
            optionID.Add(i);
            var btn = Instantiate(optionButtonPrefab, optionParent);
            var room = RunManager.Instance.DisplayRoomName(i);
            var roomType = GetRoomType(room);
            var roomPreset = lobbyPreset._lookup(roomType);
            Debug.Log($"{room.roomName}");
            btn.Init(i, roomPreset);
            if (room.roomType == Room.RoomType.Battle || room.roomType == Room.RoomType.Boss)
            {
                var battleRoom = (RoomBattle)room;
                btn.SetupPreviewImage(battleRoom.enemyConfig);
            }
            else
            {
                btn.preview.gameObject.SetActive(false);
            }
            btn.lobbyManager = this;
            spawnedOptions.Add(btn.gameObject);
        }
    }
    private string GetRoomType(Room room)
    {
        var roomType = room.roomType;
        string type = roomType.ToString();
        Debug.Log($"Room type: {type}");
        return type;
    }

    private void UpdateOptionVisual()
    {
        int optionCount = spawnedOptions.Count;
        for (int i = 0; i < optionCount; i++)
        {
            float horizontalOffset = (spacing * (i - (optionCount - 1) / 2f));
            spawnedOptions[i].transform.localPosition = new Vector3(horizontalOffset, 0, 0);
        }
    }
    private void ClearOptions()
    {
        foreach (var obj in spawnedOptions)
        {
            if (obj != null)
                Destroy(obj);
        }
        spawnedOptions.Clear();
        optionID.Clear();
    }
    //Khoa nut sau khi bam
    public void DisableAllButtons()
    {
        foreach (var obj in spawnedOptions)
        {
            var btn = obj.GetComponent<LobbyOptionButton>();
            btn.SetInteractable(false);
        }
        roomManager.ClearTempList();
    }
}
