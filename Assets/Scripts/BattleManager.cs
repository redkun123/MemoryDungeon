using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [Header("Slots")]
    [SerializeField] private Transform playerSlot;
    [SerializeField] private Transform enemySlots;

    [Header("Prefabs")]
    [SerializeField] private PlayerView playerViewPrefab;
    [SerializeField] private EnemyView enemyViewPrefab;
    [SerializeField] public EnergyView energyView;

    Player player;
    public Enemy enemy;
    BattleLogic battleLogic;
    [SerializeField] EnemyConfig enemyConfig;
    public bool isPlayerTurn;
    public bool isBattleEnd;

    // Update is called once per frame
    void Update()
    {
        if (isBattleEnd) return;
        CheckGameResult();
    }
    public EnergyView SetupEnergy()
    {
        return energyView;
    }
    public Enemy StartBattle(Player player)
    {
        isPlayerTurn = true;
        isBattleEnd = false;
        this.player = player;
        return enemy = EnemyFactory.Create(enemyConfig);
    }
    public void EndPlayerTurn()
    {
        if (isBattleEnd)
            return;

        if (!isPlayerTurn)
            return;

        isPlayerTurn = false;

        StartEnemyTurn();
    }
    private void StartEnemyTurn()
    {
        if (isBattleEnd) return;
        enemy.Attack(player);
        CheckGameResult();
        if (!isBattleEnd)
        {
            StartPlayerTurn();
        }
    }
    private void StartPlayerTurn()
    {
        isPlayerTurn = true;
        player.RestoreEnergy();
        battleLogic.RefillHand();
    }
    public void CheckGameResult()
    {
        if (!player.isAlive || !enemy.isAlive)
        {
            isBattleEnd = true;
            EndLevel();
        }
    }

    public void EndLevel()
    {
        if (!player.isAlive)
        {
            Lose();
        }
        else if (!enemy.isAlive)
        {
            Win();
        }
        else
        {
            return;
        }
    }
    public void Lose()
    {
        Debug.Log("You lost.");
    }
    public void Win()
    {
        Debug.Log("You win!");
    }
}
