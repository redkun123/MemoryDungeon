using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkipIntroButton : MonoBehaviour
{
    [SerializeField] private Button _skipIntroButton;
    public void Awake()
    {
        _skipIntroButton.onClick.AddListener(OnClickSkip);
    }
    public void OnClickSkip()
    {
        GameManager.Instance.StartRun();
    }
}
