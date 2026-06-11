using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPopup : MonoBehaviour
{
    [SerializeField] private List<Transform> cardSlotPos;
    [SerializeField] private List<Transform> relicSlotPos;
    [SerializeField] private ShopSlot cardSlotPrefab;
    [SerializeField] private ShopSlot relicSlotPrefab;
    [SerializeField] private RemoveCardButton removeCardButton;

    private List<ShopSlot> activeSlots;
    public void LoadCard(List<Card> card, Dictionary<Card, int> cardPrice)
    {
        activeSlots.Clear();
        for (int i = 0; i < card.Count; i++)
        {
            Card item = card[i];
            int price = cardPrice[item];
            ShopSlot cardSlot = Instantiate(cardSlotPrefab, cardSlotPos[i]);
            cardSlot.Init(item, price);
            activeSlots.Add(cardSlot);
        }
    }
    public void Init()
    {
        activeSlots = new List<ShopSlot>();
        RunManager.Instance.player.OnTrueDeckRemove += RemoveRemoveButton;
    }
    public void SetSlotID()
    {
        for (int i = 0; i < activeSlots.Count; i++)
        {
            activeSlots[i].id = i;
        }
    }
    public void LoadRelic(List<RelicData> relic, Dictionary<RelicData,int> relicPrice)
    {
        for (int i = 0; i < relic.Count; i++)
        {
            RelicData item = relic[i];
            int price = relicPrice[item];
            ShopSlot relicSlot = Instantiate(relicSlotPrefab, relicSlotPos[i]);
            relicSlot.Init(item, price);
            activeSlots.Add(relicSlot);
        }
    }
    public void BuySuccess(int slotID)
    {
        if (slotID < 0 || slotID >= activeSlots.Count)
            return;

        activeSlots[slotID].gameObject.SetActive(false);
        Debug.Log("Bought item.");
    }
    public List<ShopSlot> GetSlots()
    {
        return activeSlots;
    }
    public void RemoveRemoveButton()
    {
        removeCardButton.gameObject.SetActive(false);
    }
    private void OnDestroy()
    {
        RunManager.Instance.player.OnTrueDeckRemove -= RemoveRemoveButton;
    }
}
