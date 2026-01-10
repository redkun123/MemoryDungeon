using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    private Player player;
    [SerializeField] DeckBattleUI deckBattleUI;
    [SerializeField] DiscardUI discardUI;

    public void Bind(Player player)
    {
        this.player = player;
        UpdateDeckCount();
        UpdateDiscardCount();
    }
    public void UpdateDeckCount()
    {
        player.OnDeckChanged += deckBattleUI.Set;
        deckBattleUI.Set(player.deck.Count);
    }
    public void UpdateDiscardCount()
    {
        player.OnDiscardChanged += discardUI.Set;
        discardUI.Set(player.discard.Count);
    }
}
