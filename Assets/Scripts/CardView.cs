using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text costText;
    [SerializeField] TMP_Text descText;

    private Card card;

    public void Bind(Card card)
    {
        this.card = card;
        nameText.text = card.CardName;
        costText.text = card.EnergyCost.ToString();
        //descText.text = card.Description;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Card clicked: " + card.CardName);
        // gửi event cho BattleLogic / HandController
    }
}

