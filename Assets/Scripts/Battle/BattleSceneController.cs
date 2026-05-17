using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(1)]
public class BattleSceneController : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private BattleManager battleManager;
    [Header("Spawn Slots")]
    [SerializeField] private Transform playerSlot;
    [SerializeField] private Transform enemySlot;
    [SerializeField] private Transform handArea;

    [Header("Prefabs")]
    [SerializeField] private PlayerView playerViewPrefab;
    [SerializeField] private EnemyView enemyViewPrefab;
    [SerializeField] private EnergyView energyView;
    [SerializeField] private DeckManager deckManager;

    public Player Player { get; private set; }
    public Enemy Enemy { get; private set; }
    public PlayerView playerView;
    public EnemyView enemyView;

    private void Awake()
    {
        RunManager.Instance.RegisterBattleScene(this);
    }

    private void OnDestroy()
    {
        if (RunManager.Instance != null)
            RunManager.Instance.UnregisterBattleScene(this);
    }
    public void BattleSceneStart()
    {
        Player = RunManager.Instance.player;
        Enemy = RunManager.Instance.enemy;

        // Spawn Player
        playerView = Instantiate(playerViewPrefab,playerSlot.position,Quaternion.identity,playerSlot);
        playerView.Bind(Player);

        // Spawn Enemy
        enemyView = Instantiate(enemyViewPrefab,enemySlot.position,Quaternion.identity,enemySlot);
        enemyView.Bind(Enemy, battleManager);


        energyView.Bind(Player);
        deckManager.Bind(Player);

    }
}
