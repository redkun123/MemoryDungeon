using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuitToMainButton : MonoBehaviour
{
    public Button _quitButton;
    public void Awake()
    {
        _quitButton.onClick.RemoveAllListeners();
        _quitButton.onClick.AddListener(BackToLoading);
    }
    private void BackToLoading()
    {
        RunManager.Instance.UpdateRunSave();
        SceneManager.LoadScene("LoadingScene");
    }
    private void OnDestroy()
    {
        _quitButton?.onClick.RemoveListener(BackToLoading);
    }
}
