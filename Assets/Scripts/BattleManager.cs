using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [Header("Battle Managers")]
    [SerializeField] private HandManager handManager;

    [Header("Slots")]
    [SerializeField] private Transform playerSlot;
    [SerializeField] private Transform enemySlots;
    [SerializeField] private Transform handArea;

    [Header("Prefabs")]
    [SerializeField] private PlayerView playerViewPrefab;
    [SerializeField] private EnemyView enemyViewPrefab;

    Player player;
    public Enemy enemy;
    public BattleLogic battleLogic = new();
    [SerializeField] EnemyConfig enemyConfig;
    public bool isPlayerTurn;
    public bool isBattleEnd;

    // Update is called once per frame
    void Update()
    {
        if (isBattleEnd) return;
        CheckGameResult();
    }

    public void StartBattle(Player player)
    {
        isPlayerTurn = true;
        isBattleEnd = false;
        this.player = player;
        Extensions.Shuffle(player.deck);
        player.hand = new List<Card>();
        player.discard = new List<Card>();
        enemy = CreateEnemy();
    }
    public Enemy CreateEnemy()
    {
        return enemy = EnemyFactory.Create(enemyConfig);
    }
    public void EndPlayerTurn()
    {
        if (isBattleEnd)
            return;

        if (!isPlayerTurn)
            return;

        isPlayerTurn = false;
        player.DiscardAll();
        handManager.RemoveAll();
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
    public void StartPlayerTurn()
    {
        Debug.Log("Player turn started");
        isPlayerTurn = true;
        player.RestoreEnergy(player.maxEnergy);
        Debug.Log($"True Energy: {player.currentEnergy} / {player.maxEnergy}");
        Debug.Log($"handManager = {handManager}");
        Debug.Log($"battleLogic = {battleLogic}");
        battleLogic.RefillHand(handManager, player);
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
