using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextRoomButton : MonoBehaviour
{
    [SerializeField] private Button _nextRoomButton;
    public void Awake()
    {
        _nextRoomButton.onClick.AddListener(OnClickNextRoom);
    }
    public void OnClickNextRoom()
    {
        RunManager.Instance.InitNextRoom();
    }
}
