using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSceneController : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;    
    [Header("Spawn Slots")]
    [SerializeField] private Transform playerSlot;
    [SerializeField] private Transform enemySlot;

    [Header("Prefabs")]
    [SerializeField] private PlayerView playerViewPrefab;
    [SerializeField] private EnemyView enemyViewPrefab;
    [SerializeField] private EnergyView energyView;

    private void Start()
    {
        Player player = gameManager.GetPlayer();
        Enemy enemy = gameManager.GetEnemy();

        // Spawn Player
        PlayerView playerView = Instantiate(playerViewPrefab,playerSlot.position,Quaternion.identity,playerSlot);
        playerView.Bind(player);

        // Spawn Enemy
        EnemyView enemyView = Instantiate(enemyViewPrefab,enemySlot.position,Quaternion.identity,enemySlot);
        enemyView.Bind(enemy);

        EnergyView energyView = gameManager.GetEnergy();
        energyView.Bind(player);
    }
}
