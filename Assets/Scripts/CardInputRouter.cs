using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardInputRouter : MonoBehaviour
{
    public static CardInputRouter Instance { get; private set; }
    [SerializeField] private CardController cardController;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    public void OnCardClick(CardDisplay cardUI, PointerEventData cardClickEvent)
    {
        if (cardUI == null) return;
        cardController.CardClick(cardUI);
    }
    public void OnConfirmClick(PointerEventData cardConfirmEvent)
    {
        Debug.Log("Confirm click.");
        cardController.CardConfirm();
    }
}
