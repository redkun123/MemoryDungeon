using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    [SerializeField] public Button _continueButton;

    private void Awake()
    {
        _continueButton.onClick.RemoveAllListeners();
        _continueButton.onClick.AddListener(OnClickContinue);
    }
    public void OnClickContinue()
    {
        RunManager.Instance.ResumeRun();
    }
    public void OnDestroy()
    {
        
    }
}
