using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BattleLogic
{
    public Player player;
    public Enemy enemy;
    public int maxHandSize = 6;
    public CardContext CreateContext(Card card, Character target)
    {
        return new CardContext(player, target, card);
    }
    public void RefillHand()
    {
        int need = maxHandSize - player.hand.Count;
        for (int i = 0; i < need; i++)
        {
            player.DrawOne();
        }
    }
    public bool CanPlayCard(Card card)
    {
        if (!player.hand.Contains(card)) return false;
        if (player.currentEnergy < card.EnergyCost)
        {
            Debug.Log("Not enough Energy!");
            return false;
        }
        return true;
    }

    public void PlayCard(Card card, Character target)
    {
        if (!CanPlayCard(card)) return;
        player.SpendEnergy(card.EnergyCost);
        player.UseCard(card, CreateContext(card, target));
        player.Discard(card);
    }
}

