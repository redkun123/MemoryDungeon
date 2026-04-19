using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInventory
{
    public List<Card> cardForSale;
    public int cardSaleCount;
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
}
