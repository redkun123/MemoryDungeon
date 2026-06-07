using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AbandonRunButton : MonoBehaviour
{
    [SerializeField] private Button _abandonRunButton;
    void Awake()
    {
        _abandonRunButton.onClick.RemoveAllListeners();
        _abandonRunButton.onClick.AddListener(AbandonRun);
    }
    void AbandonRun()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        Debug.Log(sceneName);
        
        if (sceneName == "MainScreen")
        {
            RunManager.Instance.AbandonRun();
            Debug.Log("Run abandoned!");
        }
        else
        {
            RunManager.Instance.EndRun();
            Debug.Log("Run abandoned!");
        }
    }
    private void OnDestroy()
    {
        _abandonRunButton.onClick.RemoveAllListeners();
    }
}
