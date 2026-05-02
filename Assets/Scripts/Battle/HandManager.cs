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
    List<CardDisplay> cardInHand;

    private void OnEnable()
    {
        player = RunManager.Instance.player;
        cardInHand = new List<CardDisplay>();
        player.OnDraw -= DrawOne;
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
        Debug.Log($"Card {cardUI.cardName} go to discard pile");
        cardInHand.Remove(cardUI);
        Destroy(cardUI.gameObject);
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
        cardInHand.RemoveAll(card => card == null);
        int cardCount = cardInHand.Count;
        for (int i = 0; i < cardCount; i++)
        {
            float horizontalOffset = (spacing * (i - (cardCount - 1) / 2f));
            cardInHand[i].transform.localPosition = new Vector3(horizontalOffset, 0, 0);
        }   
    }

    public CardDisplay CreateCard(Card cardData)
    {
        CardDisplay cardPrefab = Instantiate(cardUI, handArea);
        cardPrefab.SetupCard(cardData);
        return cardPrefab;
    }
    private void OnDisable()
    {
        if (player != null)
            player.OnDraw -= DrawOne;
    }
    public void ResetHand()
    {
        for (int i = cardInHand.Count - 1; i >= 0; i--)
        {
            if (cardInHand[i] != null)
                Destroy(cardInHand[i].gameObject);
        }
        cardInHand.Clear();
    }
}
