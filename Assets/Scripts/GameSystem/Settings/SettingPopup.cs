using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingPopup : MonoBehaviour
{
    [SerializeField] Button _mainMenuButton;
    [SerializeField] Button _abandonButton;
    [SerializeField] GameObject popup;
    public void SetupPopup()
    {
        SetupMainMenuButton();
        SetupAbandonButton();
    }
    private void SetupMainMenuButton()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        Debug.Log(sceneName);
        if (sceneName == "MainScreen")
        {
            _mainMenuButton.gameObject.SetActive(false);
        }
        else
        {
            _mainMenuButton.gameObject.SetActive(true);
        }
    }
    private void SetupAbandonButton()
    {
        var run = SaveManager.Instance.CurrentRun;
        if (run == null)
        {
            _abandonButton.gameObject.SetActive(false);
        }
        else
        {
            _abandonButton.gameObject.SetActive(true);
        }
        _abandonButton.onClick.AddListener(ClosePopup);
    }
    private void ClosePopup()
    {
        popup.SetActive(false);
    }
}
