using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private RunManager runManager;
    [SerializeField] private BattleManager battleManager;

    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        runManager.StartBattle();
    }
    public Player GetPlayer()
    {
        return runManager.player;
    }
    public Enemy GetEnemy()
    {
        return battleManager.enemy;
    }
}
