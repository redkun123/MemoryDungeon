using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shop/Card Price Database")]
public class ShopItem : ScriptableObject
{
    [Header("Card")]
    public List<Card> allCard;
    public Dictionary<Card, int> allCardPrice;
    [SerializeField] List<CardPriceEntry> cardItem;

    [Header("Relic")]
    public List<RelicData> allRelic;
    public Dictionary<RelicData, int> allRelicPrice;
    [SerializeField] List<RelicPriceEntry> relicItem;
    public enum ShopItemType
    {
        None,
        Card,
        Relic
    }
    void OnEnable()
    {
        CreateCardDict();
        CreateRelicDict();
    }
    void CreateCardDict()
    {
        allCardPrice = new Dictionary<Card, int>();
        allCard = new List<Card>();
        for (int i = 0; i < cardItem.Count; i++)
        {
            allCard.Add(cardItem[i].card);
            allCardPrice.Add(cardItem[i].card, cardItem[i].price);
        }
    }
    void CreateRelicDict()
    {
        allRelicPrice = new Dictionary<RelicData, int>();
        allRelic = new List<RelicData>();
        for (int i = 0; i < relicItem.Count; i++)
        {
            allRelic.Add(relicItem[i].relic);
            allRelicPrice.Add(relicItem[i].relic, relicItem[i].price);
        }
    }
}
[System.Serializable]
public class CardPriceEntry
{
    public Card card;
    public int price;
}

[System.Serializable]
public class RelicPriceEntry
{
    public RelicData relic;
    public int price;
}
