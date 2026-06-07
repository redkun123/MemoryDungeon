using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenSettingPopup : MonoBehaviour
{
    [SerializeField] private Button settingButton;
    [SerializeField] private SettingPopup popupPrefab;
    private SettingPopup popup;
    private void Awake()
    {
        settingButton.onClick.RemoveAllListeners();
        settingButton.onClick.AddListener(ShowPopup);
    }
    private void ShowPopup()
    {
        if (popup == null)
        {
            Vector2 spawnPos = new Vector2(0,0);
            popup = Instantiate(popupPrefab, spawnPos, Quaternion.identity);
            popup.SetupPopup();
            popup.gameObject.SetActive(true);
        }
        else
        {
            popup.gameObject.SetActive(true);
        }
    }
    private void OnDestroy()
    {
        settingButton.onClick.RemoveAllListeners();
    }
}
