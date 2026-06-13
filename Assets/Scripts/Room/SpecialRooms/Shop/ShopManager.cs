using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private ShopPopup shopPopupPrefab;
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private ShopItem shopItem;
    public List<Card> allCard;
    public List<Card> cardForSale;
    public Dictionary<Card, int> allCardPrice;
    public List<RelicData> allRelic;
    public List<RelicData> relicForSale;
    public Dictionary<RelicData, int> allRelicPrice;
    public ShopInventory shopInventory;
    private GameObject popup;
    public ShopPopup shopPopup;
    Player player;
    RelicManager relicManager;

    private void Awake()
    {
        InitShop();
    }
    public void InitShop()
    {
        SetupCard();
        SetupRelic();
        var spawnPos = new Vector3(0, 0, 0);
        shopPopup = Instantiate(shopPopupPrefab, spawnPos, Quaternion.identity);
        popup = shopPopup.gameObject;
        shopPopup.Init();
        SetupShop();
    }
    public void SetupCard()
    {
        this.allCard = shopItem.allCard;
        for (int i = 0; i < allCard.Count; i++)
        {
            Debug.Log($"{allCard[i].cardName}");
        }
        this.allCardPrice = shopItem.allCardPrice;
        shopInventory = new();
        Debug.Log("Shop Inventory created.");
        this.cardForSale = shopInventory.GetCardForSale(5, allCard);
    }
    public void SetupRelic()
    {
        this.allRelic = shopItem.allRelic;
        for (int i = 0; i < allRelic.Count; i++)
        {
            Debug.Log($"{allRelic[i].relicName}");
        }
        this.allRelicPrice = shopItem.allRelicPrice;
        shopInventory = new();
        Debug.Log("Shop Inventory created.");
        this.relicForSale = shopInventory.GetRelicForSale(3, allRelic);
    }
    public void SetupShop()
    {
        shopPopup.LoadCard(cardForSale, allCardPrice);
        Debug.Log("Bind card slots to shop manager");
        shopPopup.LoadRelic(relicForSale, allRelicPrice);
        Debug.Log("Bind relic slots to shop manager");
        shopPopup.SetSlotID();
        foreach (var slot in shopPopup.GetSlots())
        {
            slot.OnBuyClicked += HandleBuy;
        }
    }
    public void SetPopupActive()
    {
        if (popup == null)
        {
            InitShop();
        }
        else if (!popup.activeSelf)
        {
            Debug.Log("Trying to set popup to true");
            popup.SetActive(true);
        }
        else
        {
            return;
        }
    }
    public void HandleBuy(ShopSlot slot)
    {
        player = RunManager.Instance.player;
        relicManager = RunManager.Instance.relicManager;
        switch (slot.itemType)
        {
            case "Card":
                HandleCard(slot);
                break;
            case "Relic":
                HandleRelic(slot);
                break;
            default:
                Debug.Log("Can't define this item.");
                break;
        }
    }
    private void HandleCard(ShopSlot slot)
    {
        var card = slot.cardForSell;
        var price = slot.price;
        var slotID = slot.id;
        //Add card vao tai khoan cua player
        if (player.gold >= price)
        {
            player.ModifyDeck(card);
            shopPopup.BuySuccess(slotID);
            var p = price * -1;
            player.ModifyGold(p);
            Debug.Log("Buy card success");
        }
        else
        {
            Debug.Log("Not enough gold.");
            return;
        }
    }
    private void HandleRelic(ShopSlot slot)
    {
        var relic = slot.relicForSell;
        var price = slot.price;
        var slotID = slot.id;
        //Add relic vao tai khoan cua player
        if (player.gold >= price)
        {
            relicManager.AddRelicByID(relic.relicName);
            shopPopup.BuySuccess(slotID);
            var p = price * -1;
            player.ModifyGold(p);
            Debug.Log("Buy relic success");
        }
        else
        {
            Debug.Log("Not enough gold.");
            return;
        }
    }
    //public void BindSlot(ShopSlot slot)
    //{
    //    slot.OnBuyClicked += HandleBuy;
    //}
    private void OnDestroy()
    {
        Destroy(popup);
    }
}
