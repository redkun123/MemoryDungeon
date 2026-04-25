using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckUIManager : MonoBehaviour
{
    public static DeckUIManager Instance;
    private Player player;
    [SerializeField] GameObject popup;
    [SerializeField] private Transform content;
    [SerializeField] private CardDisplay cardPrefab;
    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void RegisterPlayer(Player player)
    {
        this.player = player;
        RefreshDeck(player.trueDeck);
        player.OnTrueDeckChange += RefreshDeck;
    }
    public void ShowTrueDeck()
    {
        OpenDeck(player.trueDeck);
    }
    public void ShowBattleDeck()
    {

    }
    public void ShowDiscard()
    {

    }
    public void ShowExhaust()
    {

    }
    public void OpenDeck(List<Card> deck)
    {
        popup.SetActive(true);
    }

    public void CloseDeck()
    {
        popup.SetActive(false);
    }
    public void RefreshDeck(List<Card> deck)
    {
        Debug.Log($"Refreshing Deck: {deck}");
        // render
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        // spawn card
        foreach (var card in deck)
        {
            var uiCard = Instantiate(cardPrefab, content);
            uiCard.SetupCard(card);
        }
    }    
}
