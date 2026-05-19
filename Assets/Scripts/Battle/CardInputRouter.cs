using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardInputRouter : MonoBehaviour
{
    public static CardInputRouter Instance { get; private set; }
    private CardController cardController;
    [SerializeField] private RemoveCardConfirmPopup confirmPopupPrefab;
    private RemoveCardConfirmPopup confirmPopup;
    public CardInputMode currentMode = CardInputMode.None;
    public CardInputMode oldMode = CardInputMode.None;
    public enum CardInputMode
    {
        None,
        Battle,
        Remove,
        View
    }
    public void SetMode(CardInputMode mode)
    {
        SavePreviousMode();
        currentMode = mode;
    }
    public void SavePreviousMode()
    {
        oldMode = currentMode;
    }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void SetupBattle(CardController cardController)
    {
        this.cardController = cardController;
        currentMode = CardInputMode.Battle;
    }
    public void OnCardClick(CardDisplay cardUI, PointerEventData cardClickEvent)
    {
        switch (currentMode)
        {
            case CardInputMode.Battle:
                OnBattleCardSelect(cardUI);
                break;
            case CardInputMode.Remove:
                OnRemoveSelect(cardUI);
                break;
            case CardInputMode.View:
                OnViewSelect(cardUI);
                break;
            default:
                Debug.Log("No card selected");
                break;
        }
    }
    public void OnRemoveSelect(CardDisplay cardUI)
    {
        if (confirmPopup != null)
        {
            confirmPopup.gameObject.SetActive(true);
        }
        else
        {
            confirmPopup = Instantiate(confirmPopupPrefab);
        }
        confirmPopup.cardData = cardUI.cardData;
    }
    public void OnViewSelect(CardDisplay cardUI)
    {
        //phong to la bai
    }
    public void OnBattleCardSelect(CardDisplay cardUI)
    {
        if (cardUI == null) return;
        cardController.CardClick(cardUI);
    }
    public void OnBattleConfirmClick(PointerEventData cardConfirmEvent)
    {
        if (currentMode != CardInputMode.Battle) return;
        Debug.Log("Confirm click.");
        cardController.CardBattleConfirm();
    }
    public void OnCardHover(CardDisplay cardUI)
    {
        cardUI.HoverVisual(true);
    }
    public void OnCardUnhover(CardDisplay cardUI)
    {

        cardUI.HoverVisual(false);
    }

    public void OnCardBeginDrag(CardDisplay cardUI)
    {
        cardUI.droppedOnConfirmArea = false;
        cardUI.BeginDragVisual();
        cardController.CardSelect(cardUI);
    }
    public void OnReleaseConfirmArea(CardDisplay cardUI)
    {
        Debug.Log("Executing play card");
        cardController.CardBattleConfirm();
    }
    public void CardDeselect(CardDisplay cardUI)
    {
        cardController.ClearSelection();
        cardUI.ReturnToHand();
    }
}
