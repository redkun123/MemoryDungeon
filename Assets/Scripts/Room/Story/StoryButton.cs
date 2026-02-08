using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryButton
{
    public Button _triggerButton;
    StoryOption option;
    StoryManager storyManager;
    public void Init(StoryOption option, StoryManager manager)
    {
        this.option = option;
        this.storyManager = manager;
        _triggerButton.onClick.AddListener(Execute);
    }
    public void Execute()
    {
        storyManager.ExecuteChosenOption(option);
    }
}
