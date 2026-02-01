using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaveButton : MonoBehaviour
{
    public Button _restButton;
    public void Awake()
    {
        _restButton.onClick.AddListener(Leave);
    }
    public void Leave()
    {
        RunManager.Instance.RoomComplete();
    }
}
