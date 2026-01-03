using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    [SerializeField] private CardDisplay cardDefaultPrefab;
    [SerializeField] private Transform handContainer;

    public CardDisplay CreateCard(Card cardData)
    {
        CardDisplay cardPrefab = Instantiate(cardDefaultPrefab, handContainer);
        cardPrefab.SetupCard(cardData);
        return cardPrefab;
    }
}
