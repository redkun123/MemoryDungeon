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
    private Vector3 dragOffset;

    private BattleManager battleManager;

    public void Init(BattleManager battleManager)
    {
        this.battleManager = battleManager;

        rectTransform = GetComponent<RectTransform>();

        canvas = GetComponentInParent<Canvas>();

        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isDragging) return;
        if (battleManager.inputLocked) return;
        CardInputRouter.Instance.OnCardHover(cardUI);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isDragging) return;
        if (battleManager.inputLocked) return;
        CardInputRouter.Instance.OnCardUnhover(cardUI);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (battleManager.inputLocked) return;
        dragOffset = transform.position - Input.mousePosition;
        isDragging = true;
        canvasGroup.blocksRaycasts = false;
        cardUI.droppedOnConfirmArea = false;
        canvasGroup.blocksRaycasts = false;

        CardInputRouter.Instance.OnCardBeginDrag(cardUI);
    }


    public void OnDrag(PointerEventData eventData)
    {
        if (battleManager.inputLocked) return;
        transform.position = Input.mousePosition + dragOffset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (battleManager.inputLocked) return;
        isDragging = false;
        cardUI.EndDragVisual();
        canvasGroup.blocksRaycasts = true;

        if (!cardUI.droppedOnConfirmArea)
        {
            CardInputRouter.Instance.CardDeselect(cardUI);
        }
        else
        {
            Debug.Log("Skip EndDrag because already confirmed");
            return;
        }
    }
}