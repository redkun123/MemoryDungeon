using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RandomReward : MonoBehaviour
{
    public Button _triggerButton;
    public TextMeshProUGUI rewardName;
    public TextMeshProUGUI rewardAmount;
    Reward reward;
    RewardGenerator rewardGenerator;
    public void Init(Reward reward, RewardGenerator manager)
    {
        this.reward = reward;
        rewardGenerator = manager;
        rewardName.text = reward.rewardName;
        rewardAmount.text = reward.amount.ToString();
        _triggerButton.onClick.RemoveAllListeners();
        _triggerButton.onClick.AddListener(Execute);
    }
    public void Execute()
    {
        rewardGenerator.AddSpecificReward(reward);
        RunManager.Instance.RoomComplete();
    }
    void OnDestroy()
    {
        _triggerButton.onClick.RemoveListener(Execute);
    }
}
