using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyFactory
{
    public static Enemy Create(EnemyConfig config)
    {
        Enemy enemy = new Enemy();
        enemy.isAlive = true;
        enemy.maxHP = config.maxHP;
        enemy.currentHP = config.maxHP;
        enemy.moveSet = new List<Card>(config.moveSet);
        enemy.turnCount = 0;
        return enemy;
    }
}

