using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardController : MonoBehaviour
{
    [SerializeField] private BattleManager battleManager;
    [SerializeField] private HandManager handManager;
    BattleLogic battleLogic;
    private CardDisplay selectedCardUI;
    private Card selectedCardData;

    private void Start()
    {
        this.battleLogic = battleManager.battleLogic;
    }
    public void CardClick(CardDisplay cardUI)
    { 
        if (selectedCardUI == cardUI)
        {
            ClearSelection();
            return;
        }
        ClearSelection();
        CardSelect(cardUI);
    }

    public void CardBattleConfirm()
    {
        if (selectedCardUI == null) return;
        Debug.Log("Card confirmed");
        if (!battleLogic.CanPlayCard(selectedCardData))
        {
            Debug.Log("Not enough Energy");
            battleManager.cardController.ClearSelection();
            CardInputRouter.Instance.CardDeselect(selectedCardUI);
            return;
        }
        Debug.Log("Card validated");
        CardDisplay ui = selectedCardUI;
        Card data = selectedCardData;
        ClearSelection();
        StartCoroutine(battleLogic.PlayCard(data, battleManager.enemy));
        handManager.RemoveCardFromHand(ui);
    }

    public void CardSelect(CardDisplay cardUI)
    {
        selectedCardUI = cardUI;
        selectedCardData = cardUI.cardData;
        selectedCardUI.CardHighlight(true);
        Debug.Log("Card selected");
    }

    public void ClearSelection()
    {   
        if (selectedCardUI == null) return;
        selectedCardUI.CardHighlight(false);
        selectedCardUI = null;
        selectedCardData = null;
        Debug.Log("Card deselected");
    }
}
