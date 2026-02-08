using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Room;

public class RestButton : MonoBehaviour
{
    public Button _restButton;
    public RoomRest roomRest;
    public void Awake()
    {
        _restButton.onClick.AddListener(Rest);
    }
    public void Rest()
    {
        RunManager.Instance.Rest();
    }
}
