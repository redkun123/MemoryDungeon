using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Option2Button : MonoBehaviour
{
    [SerializeField] private Button _option1Button;
    [SerializeField] private TextMeshProUGUI roomName;
    public void Awake()
    {
        _option1Button.onClick.AddListener(OnClickNextRoom);
        roomName.text = RunManager.Instance.DisplayRoomName(1);
    }
    public void OnClickNextRoom()
    {
        RunManager.Instance.BindRandomRoom(1);
    }
}
