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
    [SerializeField] private WinLosePopup winLosePopup;

    Player player;
    public Enemy enemy;
    public BattleLogic battleLogic = new();
    [SerializeField] EnemyConfig enemyConfig;
    public bool isPlayerTurn;
    public event Action OnPlayerTurn;


    public void StartBattle(Player player)
    {
        this.player = player;
        Extensions.Shuffle(player.deck);
        player.hand = new List<Card>();
        player.discard = new List<Card>();
        enemy = CreateEnemy();
        player.Dies += CheckGameResult;
        enemy.Dies += CheckGameResult;
        isPlayerTurn = true;
    }
    public Enemy CreateEnemy()
    {
        return enemy = EnemyFactory.Create(enemyConfig);
    }
    public void EndPlayerTurn()
    {
        if (!isPlayerTurn) return;
        isPlayerTurn = false;
        player.DiscardAll();
        handManager.RemoveAll();
        StartEnemyTurn();
    }
    private void StartEnemyTurn()
    {
        battleLogic.EnemyActionPerTurn(enemy, player);
        StartPlayerTurn();
    }
    public void StartPlayerTurn()
    {
        Debug.Log("Player turn started");
        isPlayerTurn = true;
        OnPlayerTurn?.Invoke();
        player.RestoreEnergy(player.maxEnergy);
        Debug.Log($"True Energy: {player.currentEnergy} / {player.maxEnergy}");
        battleLogic.RefillHand(handManager, player);
    }

    public void CheckGameResult()
    {
        if (!player.isAlive)
        {
            Lose();
        }
        else
        {
            Win();
        }
    }
    public void Lose()
    {
        WinLosePopup popup = Instantiate(winLosePopup,new Vector3(0, 0, 0), Quaternion.identity);
        popup.result.text = "GAME OVER";
        Debug.Log("You lost.");
    }
    public void Win()
    {
        WinLosePopup popup = Instantiate(winLosePopup, new Vector3(0, 0, 0), Quaternion.identity);
        popup.result.text = "YOU WIN!!!";
        Debug.Log("You win!");
    }
}
