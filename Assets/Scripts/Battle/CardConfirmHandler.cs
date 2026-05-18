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
    public void OnDrop(PointerEventData eventData)
    {
        var go = eventData.pointerDrag;
        if (go == null)
        {
            Debug.LogWarning("OnDrop: pointerDrag is null");
            return;
        }

        CardDisplay cardUI = go.GetComponent<CardDisplay>();

        if (cardUI == null)
        {
            Debug.LogWarning("OnDrop: no CardDisplay found");
            return;
        }

        cardUI.droppedOnConfirmArea = true;

        Debug.Log($"Drop confirmed: {cardUI.name}");

        CardInputRouter.Instance.OnReleaseConfirmArea(cardUI);
    }
}
