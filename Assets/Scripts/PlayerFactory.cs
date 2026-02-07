using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerFactory
{
    public static Player Create(PlayerConfig config)
    {
        Player player = new Player();
        player.isAlive = true;
        player.maxHP = config.maxHP;
        player.currentHP = config.maxHP;
        player.maxEnergy = 3;
        player.currentEnergy = player.maxEnergy;
        player.gold = config.startGold;
        player.trueDeck = new List<Card>(config.startingDeck);
        player.name = config.charName;
        return player;
    }
}

