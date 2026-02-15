using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoryButton : MonoBehaviour
{
    public Button _triggerButton;
    public TextMeshProUGUI optionText;
    StoryOption option;
    StoryManager storyManager;
    public void Init(StoryOption option, StoryManager manager)
    {
        this.option = option;
        storyManager = manager;
        optionText.text = option.optionDescription;
        _triggerButton.onClick.RemoveAllListeners();
        _triggerButton.onClick.AddListener(Execute);
    }
    public void Execute()
    {
        storyManager.ExecuteChosenOption(option);
    }
    void OnDestroy()
    {
        _triggerButton.onClick.RemoveListener(Execute);
    }
}
