using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;

public class HandManager : MonoBehaviour
{
    [SerializeField] private CardDisplay cardUI;
    [SerializeField] private Transform handArea;
    [SerializeField] public float spacing = 200f;
    [SerializeField] private Transform deckPosition;
    [SerializeField] private Transform discardPosition;
    private Player player;
    List<CardDisplay> cardInHand;

    private void Awake()
    {
        player = RunManager.Instance.player;
        if (cardInHand == null)
        {
            cardInHand = new List<CardDisplay>();
        }
        else
        {
            cardInHand.Clear();
        }
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

        // Scale animation
        newCard.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);

        //update target position
        Debug.Log($"Current number of card in hand: {cardInHand.Count}");
        UpdateHandVisual();
    }

    public void RemoveCardFromHand(CardDisplay cardUI)
    {
        SendToDiscard(cardUI);
        Debug.Log($"Card {cardUI.cardName.text} go to discard pile");
        cardInHand.Remove(cardUI);
        UpdateHandVisual();
    }
    public void RemoveAll()
    {
        Debug.Log("Trying to remove all card");
        var tempList = new List<CardDisplay>();
        tempList.AddRange(cardInHand);
        for (int i = tempList.Count - 1; i >= 0; i--)
        {
            RemoveCardFromHand(tempList[i]);
        }
        Debug.Log("All card removed");
    }

    public void UpdateHandVisual()
    {
        cardInHand.RemoveAll(card => card == null);
        int cardCount = cardInHand.Count;
        for (int j = 0; j < cardCount; j++)
        {
            Debug.Log($"Card {j} in hand: {cardInHand[j].cardName.text}");
        }
        for (int i = 0; i < cardCount; i++)
        {
            Vector2 targetPos = GetCardPosition(i, cardCount);
            RectTransform rect = cardInHand[i].GetComponent<RectTransform>();
            rect.DOAnchorPos(targetPos, 0.25f).SetEase(Ease.OutCubic);
        }
    }
    public void SendToDiscard(CardDisplay card)
    {
        RectTransform rect = card.GetComponent<RectTransform>();
        DG.Tweening.Sequence seq = DOTween.Sequence();
        seq.Join(rect.DOMove(discardPosition.position, 1f).SetEase(Ease.OutCubic));
        seq.Join(card.transform.DOScale(Vector3.zero, 3f).SetEase(Ease.OutBack));
        seq.OnComplete(() =>
        {
            Destroy(card.gameObject);
        });
    }
    private Vector2 GetCardPosition(int index, int total)
    {
        float horizontalOffset = spacing * (index - (total - 1) / 2f);
        return new Vector2(horizontalOffset, 0);
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
