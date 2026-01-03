using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI cardName;
    [SerializeField] TextMeshProUGUI cardDescription;
    [SerializeField] TextMeshProUGUI energyCost;
    //[SerializeField] Sprite cardImage;
    //[SerializeField] TextMeshProUGUI cardType;

    public void SetupCard(Card card)
    {
        cardName.text = card.cardName;
        energyCost.text = card.energyCost.ToString();
        //cardImage = card.cardImage;
        cardDescription.text = card.GetFullDescription();
    }
}

