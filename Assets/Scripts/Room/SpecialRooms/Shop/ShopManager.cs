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
    public ShopInventory shopInventory;
    private GameObject popup;
    public ShopPopup shopPopup;

    private void Awake()
    {
        InitShop();
    }
    public void InitShop()
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
        var spawnPos = new Vector3(0, 0, 0);
        shopPopup = Instantiate(shopPopupPrefab, spawnPos, Quaternion.identity);
        popup = shopPopup.gameObject;
        shopPopup.Init();
        SetupShop();
    }
    public void SetupShop()
    {
        shopPopup.LoadCard(cardForSale, allCardPrice);
        foreach (var slot in shopPopup.GetSlots())
        {
            slot.OnBuyClicked += HandleBuy;
        }
        Debug.Log("Bind slots to shop manager");
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
        Player player = RunManager.Instance.player;
        var price = slot.price;
        //if (slot.cardForSell != null && slot.relicForSell == null)
        //{

        //}
        var item = slot.cardForSell;
        var slotID = slot.id;
        //Add item (card/relic) vao tai khoan cua player
        if (player.gold >= price)
        {
            RunManager.Instance.player.ModifyDeck(item);
            shopPopup.BuySuccess(slotID);
            player.gold -= price;
            Debug.Log("Buy card success");
        }
        else
        {
            Debug.Log("Not enough gold.");
            return;
        }
    }
    public void BindSlot(ShopSlot slot)
    {
        slot.OnBuyClicked += HandleBuy;
    }
    private void OnDestroy()
    {
        Destroy(popup);
    }
}
