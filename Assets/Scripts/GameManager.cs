using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor;

[DefaultExecutionOrder(1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private RunManager runManagerPrefab;
    [SerializeField] public PlayerConfig playerConfig;
    [SerializeField] public Enemy enemy; //Tạm để test
    private RunManager _runManager;
    public RunManager RunManager => _runManager;
    //[SerializeField] private BattleManager battleManager;
    //public event 
    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void NewGame()
    {
        SceneManager.LoadScene("IntroScene");
    }
    public void StartRun()
    {
        //Tạo run mới
        RunManager runManager = Instantiate(runManagerPrefab);
        DontDestroyOnLoad(runManager.gameObject);
        Debug.Log("Run Manager created");
        runManager.StartRun();
    }
    private void Start()
    {
        //battleManager.StartPlayerTurn();
    }
    public void StartBattle()
    {
        _runManager.StartBattle(enemy);
    }
    public Player GetPlayer()
    {
        return _runManager.player;
    }
    public Enemy GetEnemy()
    {
        BattleManager battleManager = new BattleManager(); //Tạm
        return battleManager.enemy;
    }
    public void EndRun()
    {
        Destroy(_runManager.gameObject);
    }    
}
