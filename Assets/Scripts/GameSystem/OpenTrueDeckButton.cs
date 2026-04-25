using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenTrueDeckButton : MonoBehaviour
{
    [SerializeField] Button _openDeckButton;

    private void Awake()
    {
        _openDeckButton.onClick.AddListener(OnClickTrueDeck);
    }
    public void OnClickTrueDeck()
    {
        CardInputRouter.Instance.SetMode(CardInputRouter.CardInputMode.View);
        var deck = RunManager.Instance.player.trueDeck;
        DeckUIManager.Instance.OpenDeck(deck);
    }
}
