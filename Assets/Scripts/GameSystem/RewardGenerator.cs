using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RewardGenerator
{
    private Dictionary<string, Card> cardLibrary;
    private Dictionary<string, RelicData> relicLibrary;
    //Generate cac phan qua
    public RewardGenerator(CardDB cardDB, RelicLibrary relicDB)
    {
        cardLibrary = new();
        foreach (var card in cardDB.cardDatabase)
        {
            cardLibrary.Add(card.name, card);
        }
        relicLibrary = new();
        foreach (var relic in relicDB.relics)
        {
            relicLibrary.Add(relic.name, relic); ;
        }
    }
    public enum RewardType
    {
        None,
        Gold,
        Card,
        Relic,
        HP
    }
    public enum RewardRank
    {
        None,
        Normal,
        Story,
        Boss
    }
    public void AddSpecificReward(Reward reward)
    {
        switch (reward.rewardType)
        {
            case RewardType.None:
                Debug.Log("Reward type invalid");
                break;
            case RewardType.Gold:
                AddGold(reward.amount);
                break;
            case RewardType.Card:
                AddCard(reward.rewardName);
                Debug.Log("Getting card");
                break;
            case RewardType.Relic:
                AddRelic(reward.rewardName);
                break;
            default:
                Debug.Log("Reward type is unknown");
                break;
        }
    }
    public List<Reward> RequestReward(int rewardCount, RewardRank rank)
    {
        int i = rewardCount;
        Debug.Log($"Reward Count: {i}");
        List<RewardType> randomType = new()
        {
            RewardType.Relic,
            RewardType.Gold,
            RewardType.Card
        };
        List<Reward> curentReward = new();
        for (int j = 0; j < i; j++)
        {
            Reward reward = new();
            Extensions.Shuffle(randomType);
            reward.rewardType = randomType[0];
            reward.amount = RequestAmount(rank, reward.rewardType);
            reward.rewardName = RequestName(reward.rewardType);
            curentReward.Add(reward);
            Debug.Log($"Reward count: {curentReward.Count}");
        }
        return curentReward;
    }
    public int RequestAmount(RewardRank rewardRank, RewardType type)
    {
        int rewardWeight = 0;
        int amount = 0;
        if (type == RewardType.Card || type == RewardType.Relic)
        {
            amount = 1;
        }
        else
        {
            switch (rewardRank)
            {
                case RewardRank.Normal:
                    rewardWeight = 5;
                    break;
                case RewardRank.Story:
                    rewardWeight = 7;
                    break;
                case RewardRank.Boss:
                    rewardWeight = 12;
                    break;
                default:
                    Debug.Log("Can't find this reward rank");
                    break;
            }
            if (type == RewardType.Gold)
            {
                amount = rewardWeight * 10;
            }
            else if (type == RewardType.HP)
            {
                amount = rewardWeight * 3;
            }
        }
        return amount;
    }
    public string RequestName(RewardType type)
    {
        string uniqueName = null;
        switch (type)
        {
            case RewardType.Gold:
                uniqueName = "Gold";
                break;
            case RewardType.Relic:
                List<RelicData> tempRelic = relicLibrary.Values.ToList();
                Extensions.Shuffle(tempRelic);
                uniqueName = tempRelic[0].relicName;
                break;
            case RewardType.Card:
                List<Card> tempCard = cardLibrary.Values.ToList();
                Extensions.Shuffle(tempCard);
                uniqueName = tempCard[0].cardName;
                break;
            default:
                Debug.Log("Can't find this reward name");
                break;
        }
        return uniqueName;
    }
    public int GenerateGold(Reward reward)
    {
        return reward.amount;
    }
    public Card GenerateCard(Reward reward)
    {
        Card card = cardLibrary[reward.rewardName];
        return card;
    }
    public RelicData GenerateRelic(Reward reward)
    {
        Debug.Log($"Relic reward name: {reward.rewardName}");
        RelicData relic = relicLibrary[reward.rewardName];
        return relic;
    }
    public void AddGold(int amount)
    {
        RunManager.Instance.player.ModifyGold(amount);
    }
    public void AddCard(string name)
    {
        Card card = cardLibrary[name];
        RunManager.Instance.player.ModifyDeck(card);
    }
    public void AddRelic(string name)
    {
        //RelicData relic = relicLibrary[name];
        RunManager.Instance.relicManager.AddRelicByID(name);
    }
}
