using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Room;

public class RestButton : MonoBehaviour
{
    public Button _restButton;
    public Player player;
    public RoomRest roomRest;
    [SerializeField] int healCost;
    public void Awake()
    {
        _restButton.onClick.AddListener(Rest);
    }
    public void Rest()
    {
        //roomRest.
    }
}
