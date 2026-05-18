using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardConfirmHandler : MonoBehaviour, IPointerClickHandler, IDropHandler
{
    public void OnPointerClick(PointerEventData cardConfirmEvent)
    {
        CardInputRouter.Instance.OnBattleConfirmClick(cardConfirmEvent);
    }
    public void OnDrop(PointerEventData cardConfirmEvent)
    {
        CardDisplay cardUI = cardConfirmEvent.pointerDrag.GetComponent<CardDisplay>();

        if (cardUI == null) return;
        cardUI.droppedOnConfirmArea = true;
        CardInputRouter.Instance.OnReleaseConfirmArea(cardUI);
    }
}
