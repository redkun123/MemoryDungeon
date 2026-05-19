using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class CardDisplay : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI cardName;
    [SerializeField] public TextMeshProUGUI cardDescription;
    [SerializeField] public TextMeshProUGUI energyCost;
    public Card cardData;
    [SerializeField] Image cardBG;
    [SerializeField] Image cardImage;
    [SerializeField] Color defaultColor;
    [SerializeField] Color highlightColor;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] private RectTransform visual;
    private Vector3 defaultScale;
    private Vector3 defaultPos;
    //[SerializeField] TextMeshProUGUI cardType;
    public event Action ReturnCardToHand;
    public bool droppedOnConfirmArea;

    public void SetupCard(Card card)
    {
        cardData = card;
        cardName.text = cardData.cardName;
        energyCost.text = cardData.energyCost.ToString();
        cardImage.sprite = card.cardSprite;
        cardDescription.text = cardData.GetFullDescription();
    }
    public void SaveLocation()
    {
        defaultScale = visual.localScale;
        defaultPos = visual.localPosition;
    }
    public void CardHighlight(bool selectCard)
    {
        Debug.Log($"Card clicked: {cardName.text}");
        if (selectCard)
        {
            cardBG.color = highlightColor;
            Debug.Log("Change color to highlight");
        }
        else
        {
            cardBG.color = defaultColor;
            Debug.Log("Change color to normal");
        }
    }
    private void OnDestroy()
    {
        transform.DOKill(true);

        RectTransform rect =
            GetComponent<RectTransform>();

        rect.DOKill(true);
    }
    public void HoverVisual(bool active)
    {
        if (canvasGroup == null) return;
        canvasGroup.blocksRaycasts = true;
        visual.DOKill();

        if (active)
        {
            visual.DOScale(defaultScale * 1.1f, 0.15f);
            visual.DOLocalMoveY(defaultPos.y + 120f, 0.15f);
        }
        else
        {
            visual.DOScale(defaultScale, 0.15f);
            visual.DOLocalMoveY(defaultPos.y, 0.15f);
        }
    }

    public void BeginDragVisual()
    {
        visual.DOKill();

        transform.SetAsLastSibling();

        canvasGroup.blocksRaycasts = false;

        visual.DOScale(defaultScale * 1.2f, 0.1f);
        //visual.DOLocalMoveY(defaultPos.y + 60f, 0.1f);
    }

    public void EndDragVisual()
    {
        visual.DOKill();

        canvasGroup.blocksRaycasts = true;

        visual.DOScale(defaultScale, 0.15f);
        visual.DOLocalMoveY(defaultPos.y, 0.15f);
    }

    public void ReturnToHand()
    {
        canvasGroup.blocksRaycasts = true;
        //visual.anchoredPosition = Vector2.zero;
        visual.localScale = Vector3.one;
        ReturnCardToHand?.Invoke();
    }
}

