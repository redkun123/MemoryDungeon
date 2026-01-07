using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardConfirmHandler : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData cardConfirmEvent)
    {
        CardInputRouter.Instance.OnConfirmClick(cardConfirmEvent);
    }
}
