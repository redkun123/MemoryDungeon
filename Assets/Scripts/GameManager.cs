using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private RunManager runManager;
    [SerializeField] private BattleManager battleManager;
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
        Enemy enemy = GetEnemy();
        runManager.StartBattle(enemy);
    }

    public void NewGame()
    {
        SceneManager.LoadScene("IntroScene");
    }
    private void Start()
    {
        //battleManager.StartPlayerTurn();
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
