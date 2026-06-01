using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    [SerializeField] public Button _continueButton;

    private void Awake()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance._continueButton = this._continueButton;
            GameManager.Instance.StartOrResume();
        }
        else
        {
            Debug.Log("Continue Button awake first");
        }
        _continueButton.onClick.RemoveAllListeners();
        _continueButton.onClick.AddListener(OnClickContinue);
    }
    public void OnClickContinue()
    {
        RunManager.Instance.ResumeRun();
    }
}
