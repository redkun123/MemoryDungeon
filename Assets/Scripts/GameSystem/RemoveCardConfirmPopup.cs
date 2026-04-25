using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveCardConfirmPopup : MonoBehaviour
{
    public Card cardData;
    [SerializeField] private GameObject popup;
    public void ConfirmRemoveCard()
    {
        var player = RunManager.Instance.player;
        player.RemoveCard(cardData);
        ClosePopup();
    }
    public void ClosePopup()
    {
        cardData = null;
        Destroy(popup);
    }
}
