using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    [SerializeField] private CardDisplay cardUI;
    [SerializeField] private Transform handArea;
    [SerializeField] public float spacing = 200f;
    List<Card> hand;
    List<CardDisplay> cardInHand = new();

    public void AddCardToHand(Player player)
    {
        this.hand = player.hand;
        for (int i = 0; i < hand.Count; i++)
        {
            cardInHand.Add(CreateCard(hand[i]));
            Debug.Log($"Display card draw {i}");
            UpdateHandVisual();
        }
    }

    public void RemoveCardFromHand(CardDisplay cardUI)
    {
        Debug.Log("Trying to remove card");
        cardInHand.Remove(cardUI);
        Debug.Log($"Card {cardUI.cardName} go to discard pile");
        UpdateHandVisual();
    }

    public void RemoveAll()
    {
        Debug.Log("Trying to remove all card");
        foreach (CardDisplay cardUI in cardInHand)
        {
            cardInHand.Remove(cardUI);
            Debug.Log($"Card {cardUI.cardName} go to discard pile");
            UpdateHandVisual();
        }
    }

    public void UpdateHandVisual()
    {
        int cardCount = cardInHand.Count;
        for (int i = 0; i < cardCount; i++)
        {
            float horizontalOffset = (spacing * (i - (cardCount - 1) / 2f) + 50f);
            //float horizontalOffset = spacing * i;
            cardInHand[i].transform.localPosition = new Vector3(horizontalOffset, 0, 0);
        }
    }

    public CardDisplay CreateCard(Card cardData)
    {
        CardDisplay cardPrefab = Instantiate(cardUI, handArea);
        cardPrefab.SetupCard(cardData);
        return cardPrefab;
    }
}
