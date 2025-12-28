using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyFactory
{
    public static Enemy Create(EnemyConfig config)
    {
        Enemy enemy = new Enemy();
        enemy.IsAlive = true;
        enemy.MaxHP = config.maxHP;
        enemy.CurrentHP = config.maxHP;
        enemy.moveSet = new List<Card>(config.moveSet);

        return enemy;
    }
}

