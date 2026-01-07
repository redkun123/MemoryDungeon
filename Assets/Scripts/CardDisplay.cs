using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI cardName;
    [SerializeField] TextMeshProUGUI cardDescription;
    [SerializeField] TextMeshProUGUI energyCost;
    public Card cardData;
    //[SerializeField] Sprite cardImage;
    //[SerializeField] TextMeshProUGUI cardType;

    public void SetupCard(Card card)
    {
        cardData = card;
        cardName.text = cardData.cardName;
        energyCost.text = cardData.energyCost.ToString();
        //cardImage = card.cardImage;
        cardDescription.text = cardData.GetFullDescription();
    }
    public void CardHighlight(bool selectCard)
    {
        Debug.Log($"Card clicked: {cardName.text}");
    }
    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    Debug.Log("Card clicked: " + card.cardName);
    //    // gửi event cho BattleLogic / HandController
    //}
}

