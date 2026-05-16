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
    [SerializeField] public CardController cardController;

    [Header("Slots")]
    [SerializeField] private Transform playerSlot;
    [SerializeField] private Transform enemySlots;
    [SerializeField] private Transform handArea;

    [Header("Prefabs")]
    [SerializeField] private PlayerView playerViewPrefab;
    [SerializeField] private EnemyView enemyViewPrefab;
    [SerializeField] private WinLosePopup winLosePopup;

    public Player player;
    public Enemy enemy;
    public EnemyConfig enemyConfig;
    public BattleLogic battleLogic;
    public BattleExecutor battleExecutor;
    public RelicManager relicManager;
    public StatusManager playerSM;
    public StatusManager enemySM;

    public bool isPlayerTurn;
    public bool battleEnded;
    public event Action OnPlayerTurnStart;
    public event Action OnEnemyTurn;
    public event Action OnBattleStart;
    public event Action OnBattleEnd;
    public event Action OnPlayerTurnEnd;

    private void Awake()
    {
        RunManager.Instance.RegisterBattleManager(this);
        enemyConfig = RunManager.Instance.currentEnemy;
        this.player = RunManager.Instance.player;
        this.relicManager = RunManager.Instance.relicManager;
    }
    private void Start()
    {
        StartBattle(player, enemyConfig);
    }
    private void OnDestroy()
    {
        if (RunManager.Instance != null)
            RunManager.Instance.UnregisterBattleManager(this);
    }
    public void StartBattle(Player player, EnemyConfig enemycf)
    {
        battleExecutor = new();
        CardInputRouter.Instance.SetupBattle(cardController);
        battleEnded = false;
        battleLogic = new();
        battleLogic.Register(this);
        this.player = player;
        player.deck = new();
        player.deck.AddRange(player.trueDeck);
        Extensions.Shuffle(player.deck);
        player.hand = new List<Card>();
        player.discard = new List<Card>();
        enemy = CreateEnemy(enemycf);
        RunManager.Instance.GetEnemy();
        player.Dies += EndBattle;
        enemy.Dies += EndBattle;
        battleSceneController.BattleSceneStart();
        relicManager.Setup();
        RegisterStatusManager();
        OnBattleStart?.Invoke();
        handManager.ResetHand();
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
        OnPlayerTurnEnd?.Invoke();
        StartEnemyTurn();
    }
    private void StartEnemyTurn()
    {
        OnEnemyTurn.Invoke();
        if (battleEnded)
        {
            CheckGameResult();
            return;
        }
        enemy.ClearGuard();
        battleLogic.EnemyActionPerTurn(enemy, player);
        if (battleEnded)
        {
            CheckGameResult();
            return;
        }
        StartPlayerTurn();
    }
    public void StartPlayerTurn()
    {
        Debug.Log("Player turn started");
        isPlayerTurn = true;
        OnPlayerTurnStart?.Invoke();
        player.ClearGuard();
        player.RestoreEnergy(player.maxEnergy);
        Debug.Log($"True Energy: {player.currentEnergy} / {player.maxEnergy}");
        StartCoroutine(battleLogic.RefillHand(handManager, player));
        OnPlayerTurnStart?.Invoke();
        if (battleEnded)
        {
            CheckGameResult();
            return;
        }
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
        RunManager.Instance.UnregisterEnemy(enemy, enemyConfig);
    }
    public void EndBattle()
    {
        OnBattleEnd?.Invoke();
        battleEnded = true;
    }
    public void Lose()
    {
        WinLosePopup popup = Instantiate(winLosePopup, new Vector3(0, 0, 0), Quaternion.identity);
        popup.result.text = "GAME OVER";
        Debug.Log("You lost.");
        RunManager.Instance.EndRun();
    }
    public void Win()
    {
        /*WinLosePopup popup = Instantiate(winLosePopup, new Vector3(0, 0, 0), Quaternion.identity);
        popup.result.text = "YOU WIN!!!";
        Debug.Log("You win!");*/

        //Tạm thời gen ra phần thưởng normal
        RunManager.Instance.GenerateRandomReward(RewardGenerator.RewardRank.Normal);
        //RunManager.Instance.RoomComplete();
    }
    private void RegisterStatusManager()
    {
        playerSM = player.statusManager;
        enemySM = enemy.statusManager;
        OnBattleEnd += playerSM.OnBattleEnd;
        OnPlayerTurnStart += playerSM.OnTurnStart;
        OnPlayerTurnEnd += playerSM.OnTurnEnd;
        OnBattleEnd += enemySM.OnBattleEnd;
        OnPlayerTurnStart += enemySM.OnTurnStart;
        OnBattleEnd += enemySM.OnBattleEnd;
    }
}
