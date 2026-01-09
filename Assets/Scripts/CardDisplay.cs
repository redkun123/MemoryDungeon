using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI cardName;
    [SerializeField] public TextMeshProUGUI cardDescription;
    [SerializeField] public TextMeshProUGUI energyCost;
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
}

