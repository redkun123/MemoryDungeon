using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Option1Button : MonoBehaviour
{
    [SerializeField] private Button _option1Button;
    [SerializeField] private TextMeshProUGUI roomName;
    public void Awake()
    {
        _option1Button.onClick.AddListener(OnClickNextRoom);
        roomName.text = RunManager.Instance.DisplayRoomName(0);
    }
    public void OnClickNextRoom()
    {
        RunManager.Instance.BindRandomRoom(0);
    }
}
