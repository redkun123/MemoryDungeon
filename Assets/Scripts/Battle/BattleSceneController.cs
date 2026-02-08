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
    //[SerializeField] private DeckBattleUI deckBattleUI;

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
        Player player = RunManager.Instance.player;
        Enemy enemy = RunManager.Instance.enemy;

        // Spawn Player
        PlayerView playerView = Instantiate(playerViewPrefab,playerSlot.position,Quaternion.identity,playerSlot);
        playerView.Bind(player);

        // Spawn Enemy
        EnemyView enemyView = Instantiate(enemyViewPrefab,enemySlot.position,Quaternion.identity,enemySlot);
        enemyView.Bind(enemy, battleManager);


        energyView.Bind(player);
        deckManager.Bind(player);

    }
}
