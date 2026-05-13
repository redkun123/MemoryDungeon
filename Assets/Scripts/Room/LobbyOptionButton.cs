using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyOptionButton : MonoBehaviour
{
    [SerializeField] private Button _optionButton;
    [SerializeField] private TextMeshProUGUI roomName;
    [SerializeField] private Image roomIcon;
    [SerializeField] private Image roomBG;

    public LobbyManager lobbyManager;
    private int optionID;
    public void Init(int option, RoomPreset room)
    {
        if (_optionButton == null || roomName == null)
        {
            Debug.LogError("LobbyOptionButton missing reference!");
            return;
        }
        optionID = option;
        _optionButton.onClick.RemoveAllListeners();
        _optionButton.onClick.AddListener(OnClickNextRoom);
        roomName.text = room.roomName;
        roomIcon.sprite = room.roomIcon;
        roomBG.sprite = room.roomBG;
    }
    public void OnClickNextRoom()
    {
        Debug.Log($"Selected room ID {optionID}");
        Debug.Log($"Initiating {roomName.text}");
        RunManager.Instance.BindRandomRoom(optionID);
        lobbyManager.DisableAllButtons();
    }
    public void SetInteractable(bool value)
    {
        _optionButton.interactable = value;
    }
    private void OnDestroy()
    {
        _optionButton.onClick.RemoveAllListeners();
    }
}
