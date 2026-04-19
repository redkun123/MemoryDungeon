using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPopup : MonoBehaviour
{
    //[SerializeField] private ShopSlot removeCard;
    [SerializeField] private List<Transform> cardSlotPos;
    [SerializeField] private ShopSlot cardSlotPrefab;
    //[SerializeField] private List<ShopSlot> relicSlot;
    private List<ShopSlot> activeSlots;
    public void LoadCard(List<Card> card, Dictionary<Card, int> cardPrice)
    {
        activeSlots.Clear();
        for (int i = 0; i < card.Count; i++)
        {
            Card item = card[i];
            int price = cardPrice[item];
            ShopSlot cardSlot = Instantiate(cardSlotPrefab, cardSlotPos[i]);
            cardSlot.Init(item, price, i);
            activeSlots.Add(cardSlot);
        }
    }
    public void Init()
    {
        activeSlots = new List<ShopSlot>();
    }
    public void LoadRelic()
    {
        //for (int i = 0; i < relicSlot.Count; i++)
        //{
        //    Extensions.Shuffle(sellRelic);
        //    var item = sellRelic[0];
        //    relicSlot[i].Init(item);
        //}
    }
    public void BuySuccess(int slotID)
    {
        if (slotID < 0 || slotID >= activeSlots.Count)
            return;

        Destroy(activeSlots[slotID].gameObject);
        Debug.Log("Bought item.");
    }
    public List<ShopSlot> GetSlots()
    {
        return activeSlots;
    }
}
