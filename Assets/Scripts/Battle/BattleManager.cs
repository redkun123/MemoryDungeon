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
    public event Action OnEnemyTurnStart;
    public event Action OnBattleStart;
    public event Action OnBattleEnd;
    public event Action OnPlayerTurnEnd;
    public event Action OnEnemyTurnEnd;

    public bool inputLocked;
    private bool resolvingAction;
    private bool playerPressedEndTurn;

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
        handManager.CreateNewCard -= RegisterCardToBM;
        if (RunManager.Instance != null)
            RunManager.Instance.UnregisterBattleManager(this);
    }
    private IEnumerator BattleLoop()
    {
        yield return StartCoroutine(BattleStartPhase());

        while (!battleEnded)
        {
            yield return StartCoroutine(PlayerTurnPhase());

            if (battleEnded) yield break;

            yield return StartCoroutine(EnemyTurnPhase());
        }
    }
    private IEnumerator BattleStartPhase()
    {
        inputLocked = true;

        battleSceneController.BattleSceneStart();

        relicManager.Setup();

        RunManager.Instance.RegisterStatusUI();

        RegisterStatusManager();

        OnBattleStart?.Invoke();

        handManager.ResetHand();

        // APPLY RELIC START BATTLE
        yield return StartCoroutine(relicManager.TriggerPhase(BattlePhase.BattleStart));

        // DRAW OPENING HAND
        yield return StartCoroutine(battleLogic.RefillHand(handManager, player));

        inputLocked = false;
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
        OnBattleEnd += CheckGameResult;
        handManager.CreateNewCard += RegisterCardToBM;
        StartCoroutine(BattleLoop());
    }

    private IEnumerator PlayerTurnPhase()
    {
        Debug.Log("PLAYER TURN");

        inputLocked = true;

        isPlayerTurn = true;

        playerPressedEndTurn = false;

        player.ClearGuard();

        player.RestoreEnergy(player.maxEnergy);

        OnPlayerTurnStart?.Invoke();

        // APPLY PLAYER STATUS
        yield return StartCoroutine(player.statusManager.TriggerPhase(BattlePhase.TurnStart));

        // DRAW
        yield return StartCoroutine(battleLogic.RefillHand(handManager, player));

        inputLocked = false;

        // CHỜ PLAYER END TURN
        yield return new WaitUntil(() => playerPressedEndTurn);

        inputLocked = true;

        isPlayerTurn = false;

        player.DiscardAll();

        handManager.RemoveAll();

        OnPlayerTurnEnd?.Invoke();

        yield return StartCoroutine(relicManager.TriggerPhase(BattlePhase.TurnEnd));

        yield return StartCoroutine(player.statusManager.TriggerPhase(BattlePhase.TurnEnd));

        yield return null;
    }
    private IEnumerator EnemyTurnPhase()
    {
        Debug.Log("ENEMY TURN");

        inputLocked = true;

        OnEnemyTurnStart?.Invoke();

        enemy.ClearGuard();

        // APPLY ENEMY STATUS
        yield return StartCoroutine(enemy.statusManager.TriggerPhase(BattlePhase.TurnStart));

        yield return StartCoroutine(EnemyActionCoroutine());

        OnEnemyTurnEnd?.Invoke();

        yield return StartCoroutine(enemy.statusManager.TriggerPhase(BattlePhase.TurnEnd));

        yield return null;
    }
    private IEnumerator EnemyActionCoroutine()
    {
        battleLogic.EnemyActionPerTurn(enemy, player);

        yield return new WaitForSeconds(1f);
    }
    public void EndPlayerTurn()
    {
        if (!isPlayerTurn)
            return;

        if (inputLocked)
            return;

        playerPressedEndTurn = true;
    }

    public void RegisterCardToBM(CardDisplay card)
    {
        card.SetupBattle(this);
    }
    public Enemy CreateEnemy(EnemyConfig enemycf)
    {
        return enemy = EnemyFactory.Create(enemycf);
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
        OnEnemyTurnStart += enemySM.OnTurnStart;
        OnEnemyTurnEnd += enemySM.OnBattleEnd;
    }
}
