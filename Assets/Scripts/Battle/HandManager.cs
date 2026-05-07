using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class HandManager : MonoBehaviour
{
    [SerializeField] private CardDisplay cardUI;
    [SerializeField] private Transform handArea;
    [SerializeField] public float spacing = 100f;
    [SerializeField] private Transform deckPosition;
    [SerializeField] private Transform discardPosition;
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
        var newCard = CreateCard(card);
        cardInHand.Add(newCard);
        Debug.Log($"Display card draw {card}");

        //spawn in deck position
        newCard.transform.position = deckPosition.position;
        newCard.transform.localScale = Vector3.zero;
        cardInHand.Add(newCard);

        // Scale animation
        newCard.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);

        //update target position
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
            Vector3 targetPos = GetCardPosition(i, cardCount);
            cardInHand[i].transform.DOLocalMove(targetPos, 0.25f).SetEase(Ease.OutCubic);
        }
    }
    private Vector3 GetCardPosition(int index, int total)
    {
        float horizontalOffset = spacing * (index - (total - 1) / 2f);
        return new Vector3(horizontalOffset, 0, 0);
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
