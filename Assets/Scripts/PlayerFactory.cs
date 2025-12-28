using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerFactory
{
    public static Player Create(PlayerConfig config)
    {
        Player player = new Player();
        player.IsAlive = true;
        player.MaxHP = config.maxHP;
        player.CurrentHP = config.maxHP;
        player.maxEnergy = 3;
        player.gold = config.startGold;
        player.deck = new List<Card>(config.startingDeck);

        return player;
    }
}

