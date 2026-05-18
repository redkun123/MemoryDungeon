using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardDragHandler : MonoBehaviour,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler,
    IPointerEnterHandler,
    IPointerExitHandler
{
    [SerializeField] private CardDisplay cardUI;

    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    private bool isDragging;
    private bool isHovering;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        canvas = GetComponentInParent<Canvas>();

        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isDragging) return;

        CardInputRouter.Instance.OnCardHover(cardUI);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isDragging) return;

        CardInputRouter.Instance.OnCardUnhover(cardUI);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDragging = true;

        canvasGroup.blocksRaycasts = false;

        CardInputRouter.Instance.OnCardBeginDrag(cardUI);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition +=
            eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;

        canvasGroup.blocksRaycasts = true;

        if (!cardUI.droppedOnConfirmArea)
        {
            CardInputRouter.Instance.CardDeselect(cardUI);
        }
        else
        {
            return;
        }
    }
}