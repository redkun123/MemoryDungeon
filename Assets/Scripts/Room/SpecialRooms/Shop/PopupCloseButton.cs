using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PopupCloseButton : MonoBehaviour
{
    [SerializeField] Button _close;
    [SerializeField] GameObject popup;
    private void Awake()
    {
        _close.onClick.AddListener(OnClickClose);
    }
    private void OnClickClose()
    {
        popup.SetActive(false);
    }
}
