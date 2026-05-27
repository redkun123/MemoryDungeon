using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        enemy.name = config.charName;
        Debug.Log($"{enemy}");
        enemy.statusManager = new StatusManager(enemy);
        enemy.avatar = config.enemyAvatar;
        return enemy;
    }
}

