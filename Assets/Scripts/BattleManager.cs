using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [Header("Battle Managers")]
    [SerializeField] private HandManager handManager;
    [SerializeField] private BattleSceneController battleSceneController;

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
    public EnemyConfig enemyConfig;
    public BattleLogic battleLogic = new();

    public bool isPlayerTurn;
    public event Action OnPlayerTurn;
    public event Action OnEnemyTurn;

    private void Awake()
    {
        RunManager.Instance.RegisterBattleManager(this);
        enemyConfig = RunManager.Instance.currentEnemy;
        this.player = RunManager.Instance.player;
    }
    private void Start()
    {
        StartBattle(player,enemyConfig);
    }
    private void OnDestroy()
    {
        if (RunManager.Instance != null)
            RunManager.Instance.UnregisterBattleManager(this);
    }
    public void StartBattle(Player player, EnemyConfig enemycf)
    {
        this.player = player;
        Extensions.Shuffle(player.deck);
        player.hand = new List<Card>();
        player.discard = new List<Card>();
        enemy = CreateEnemy(enemycf);
        RunManager.Instance.GetEnemy();
        player.Dies += CheckGameResult;
        enemy.Dies += CheckGameResult;
        battleSceneController.BattleSceneStart();
        StartPlayerTurn();
    }
    public Enemy CreateEnemy(EnemyConfig enemycf)
    {
        return enemy = EnemyFactory.Create(enemycf);
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
        OnEnemyTurn.Invoke();
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
        RunManager.Instance.UnregisterBattleScene(battleSceneController);
        RunManager.Instance.UnregisterEnemy(enemy, enemyConfig);
        RunManager.Instance.UnregisterBattleManager(this) ;
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
        RunManager.Instance.RoomComplete();
    }
}
