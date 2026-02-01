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
    //private RunManager _runManager;
    //public RunManager RunManager => _runManager;
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
        ////Tạo run mới

        //Debug.Log("Previous run data cleared");
        RunManager.Instance.StartRun();
    }
    public void EndRun()
    {
        //Destroy(_runManager.gameObject);
    }    
}
