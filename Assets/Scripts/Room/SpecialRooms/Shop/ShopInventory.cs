using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInventory
{
    public List<Card> cardForSale;
    public List<RelicData> relicForSale;
    public int cardSaleCount;
    public int relicSaleCount;
    public List<Card> GetCardForSale(int cardCount, List<Card> allCard)
    {
        cardForSale = new List<Card>();
        cardSaleCount = cardCount;
        for (int i = 0; i < cardSaleCount; i++)
        {
            Extensions.Shuffle(allCard);
            Card card = allCard[0];
            cardForSale.Add(card);
        }
        return cardForSale;
    }
    public List<RelicData> GetRelicForSale(int relicCount, List<RelicData> allRelic)
    {
        relicForSale = new List<RelicData>();
        relicSaleCount = relicCount;
        for (int i = 0; i < relicSaleCount; i++)
        {
            Extensions.Shuffle(allRelic);
            RelicData relic = allRelic[0];
            relicForSale.Add(relic);
        }
        return relicForSale;
    }
}
