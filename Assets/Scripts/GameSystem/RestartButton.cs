using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    public Button _restartButton;
    public void Awake()
    {
        _restartButton.onClick.AddListener(BackToLoading);
    }
    public void BackToLoading()
    {
        SceneManager.LoadScene("LoadingScene");
    }
    private void OnDestroy()
    {
        _restartButton?.onClick.RemoveListener(BackToLoading);
    }
}

