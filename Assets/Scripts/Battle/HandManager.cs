using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    [SerializeField] private CardDisplay cardUI;
    [SerializeField] private Transform handArea;
    [SerializeField] public float spacing = 200f;
    private Player player;
    List<Card> hand;
    List<CardDisplay> cardInHand = new();

    private void Awake()
    {
        player = RunManager.Instance.player;
        player.OnDraw += DrawOne;
    }
    public void DrawOne(Card card)
    {
        cardInHand.Add(CreateCard(card));
        Debug.Log($"Display card draw {card}");
        UpdateHandVisual();
    }
    public void RemoveCardFromHand(CardDisplay cardUI)
    {
        Debug.Log("Trying to remove card");
        cardInHand.Remove(cardUI);
        Destroy(cardUI.gameObject);
        Debug.Log($"Card {cardUI.cardName} go to discard pile");
        UpdateHandVisual();
    }

    public void RemoveAll()
    {
        Debug.Log("Trying to remove all card");
        for (int i = cardInHand.Count - 1; i >= 0; i--)
        {
            CardDisplay cardUI = cardInHand[i];
            cardInHand.RemoveAt(i);
            Debug.Log($"Card {cardUI.cardName} go to discard pile");
            Destroy(cardUI.gameObject);
        }
        UpdateHandVisual();
        Debug.Log("All card removed");
    }

    public void UpdateHandVisual()
    {
        int cardCount = cardInHand.Count;
        for (int i = 0; i < cardCount; i++)
        {
            float horizontalOffset = (spacing * (i - (cardCount - 1) / 2f));
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
