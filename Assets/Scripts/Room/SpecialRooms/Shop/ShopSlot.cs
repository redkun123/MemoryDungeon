using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    [SerializeField] Button _buy;
    [SerializeField] Image itemImage;
    [SerializeField] TextMeshProUGUI priceText;
    public Card cardForSell;
    public RelicData relicForSell;
    public int price;
    public int id;
    public string itemType;
    public Action<ShopSlot> OnBuyClicked;
    private void Awake()
    {
        _buy.onClick.AddListener(() =>
        {
            OnBuyClicked?.Invoke(this);
        });
    }
    public void Init(Card card, int price, int slotID)
    {
        cardForSell = card;
        this.price = price;
        itemImage.sprite = card.cardSprite;
        priceText.text = price.ToString();
        itemType = "Card";
        id = slotID;
    }
    public void Init(RelicData relic, int price, int slotID)
    {
        relicForSell = relic;
        this.price = price;
        itemImage.sprite = relic.icon;
        priceText.text = price.ToString();
        itemType = "Relic";
        id = slotID;
    }
}
