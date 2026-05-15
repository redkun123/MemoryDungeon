using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RandomReward : MonoBehaviour
{
    public Button _triggerButton;

    [Header ("Relic")] 
    [SerializeField] private RelicUI relicIcon;

    [Header("Card")]
    [SerializeField] private CardDisplay cardIcon;

    [Header("Gold")]
    [SerializeField] private GoldDefault goldIcon;

    Reward reward;
    RewardGenerator rewardGenerator;
    public void Init(Reward reward, RewardGenerator manager)
    {
        this.reward = reward;
        rewardGenerator = manager;
        GetRewardContent();
        _triggerButton.onClick.RemoveAllListeners();
        _triggerButton.onClick.AddListener(Execute);
    }
    public void Execute()
    {
        rewardGenerator.AddSpecificReward(reward);
        RunManager.Instance.RoomComplete();
    }
    private void GetRewardContent()
    {
        switch(reward.rewardType)
        {
            case RewardGenerator.RewardType.Card:
                SetCardUI();
                break;
            case RewardGenerator.RewardType.Relic:
                SetRelicUI();
                break;
            case RewardGenerator.RewardType.Gold:
                SetGoldUI();
                break;
            default:
                break;
        }
    }
    public void SetCardUI()
    {
        Debug.Log("Setupping card...");
        Card card = rewardGenerator.GenerateCard(reward);
        cardIcon.SetupCard(card);
        cardIcon.gameObject.SetActive(true);
    }
    public void SetRelicUI()
    {
        Debug.Log("Setupping relic...");
        RelicData relic = rewardGenerator.GenerateRelic(reward);
        relicIcon.SetupRelic(relic);
        relicIcon.gameObject.SetActive(true);
    }
    public void SetGoldUI()
    {
        Debug.Log("Setupping gold...");
        var amount = rewardGenerator.GenerateGold(reward);
        goldIcon.Init(amount);
        goldIcon.gameObject.SetActive(true);
    }
    void OnDestroy()
    {
        cardIcon.gameObject.SetActive(false);
        relicIcon.gameObject.SetActive(false);
        goldIcon.gameObject.SetActive(false);
        _triggerButton.onClick.RemoveAllListeners();
    }
}
