using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BattleLogic
{
    public Player player;
    public Enemy enemy;
    public HandManager handManager;
    public int maxHandSize = 6;
    public CardContext CreateContext(Card card, Enemy enemy)
    {
        Debug.Log("Context created");
        return new CardContext(player, enemy, card);
    }
    public void RefillHand(HandManager handManager, Player player)
    {
        Debug.Log("Refill hand started");
        this.player = player;
        Debug.Log($"player.hand = {player.hand}");
        int need = maxHandSize - player.hand.Count;
        Debug.Log("Get number of card to draw");
        for (int i = 0; i < need; i++)
        {
            player.DrawOne();
            Debug.Log($"Draw {i}");
        }
        handManager.AddCardToHand(player);
    }
    public bool CanPlayCard(Card card)
    {
        if (!player.hand.Contains(card)) return false;
        if (player.currentEnergy < card.energyCost)
        {
            Debug.Log("Not enough Energy!");
            return false;
        }
        return true;
    }

    public void PlayCard(Card card, Enemy enemy)
    {
        if (!CanPlayCard(card)) return;
        Debug.Log("Card validated");
        CardContext ctx = CreateContext(card, enemy);
        player.SpendEnergy(card.energyCost);
        Debug.Log("Energy spent");
        player.UseCard(card, ctx);
        player.Discard(card);
    }
}

