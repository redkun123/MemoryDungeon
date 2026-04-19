using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Shop/Card Price Database")]
public class ShopItem : ScriptableObject
{
    public List<Card> allCard;
    public Dictionary<Card, int> allCardPrice;
    [SerializeField] List<CardPriceEntry> cardItem;
    public enum ShopItemType
    {
        None,
        Card,
        Relic
    }
    void OnEnable()
    {
        allCardPrice = new Dictionary<Card, int>();
        allCard = new List<Card>();
        for (int i = 0; i < cardItem.Count; i++)
        {
            allCard.Add(cardItem[i].card);
            allCardPrice.Add(cardItem[i].card, cardItem[i].price);
        }
    }
}
[System.Serializable]
public class CardPriceEntry
{
    public Card card;
    public int price;
}
