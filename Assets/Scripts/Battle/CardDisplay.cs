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
    [SerializeField] Image cardBG;
    [SerializeField] Image cardImage;
    [SerializeField] Color defaultColor;
    [SerializeField] Color highlightColor;
    //[SerializeField] TextMeshProUGUI cardType;

    public void SetupCard(Card card)
    {
        cardData = card;
        cardName.text = cardData.cardName;
        energyCost.text = cardData.energyCost.ToString();
        cardImage.sprite = card.cardSprite;
        cardDescription.text = cardData.GetFullDescription();
    }
    public void CardHighlight(bool selectCard)
    {
        Debug.Log($"Card clicked: {cardName.text}");
        if (selectCard)
        {
            cardBG.color = highlightColor;
            Debug.Log("Change color to highlight");
        }
        else
        {
            cardBG.color = defaultColor;
            Debug.Log("Change color to normal");
        }
    }
}

