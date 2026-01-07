using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardClickHandler : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private CardDisplay cardUI;
    public void OnPointerClick(PointerEventData eventData)
    {
        CardInputRouter.Instance.OnCardClick(cardUI, eventData);
    }
}
