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
    public int price;
    public int id;
    private string itemType;
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
    public void Init()
    {

    }
}
