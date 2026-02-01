using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option3Button : MonoBehaviour
{
    [SerializeField] private Button _option1Button;
    public void Awake()
    {
        _option1Button.onClick.AddListener(OnClickNextRoom);
    }
    public void OnClickNextRoom()
    {
        RunManager.Instance.BindRandomRoom(2);
    }
}
