using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class QuitGameButton : MonoBehaviour
{
    [SerializeField] private Button _quitGameButton;
    public void Awake()
    {
        _quitGameButton.onClick.AddListener(OnClickQuit);
    }
    public void OnClickQuit()
    {
        Application.Quit();
    }
    public void OnDestroy()
    {
        _quitGameButton.onClick.RemoveAllListeners();
    }
}
