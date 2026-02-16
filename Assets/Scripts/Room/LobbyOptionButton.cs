using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyOptionButton : MonoBehaviour
{
    [SerializeField] private Button _option1Button;
    [SerializeField] private TextMeshProUGUI roomName;
    private int optionCount;
    public void Init(int option)
    {
        optionCount = option;
        _option1Button.onClick.AddListener(OnClickNextRoom);
        roomName.text = RunManager.Instance.DisplayRoomName(optionCount);
    }
    public void OnClickNextRoom()
    {
        RunManager.Instance.BindRandomRoom(optionCount);
    }
}
