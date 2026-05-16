using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[DefaultExecutionOrder(1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public StatusBar statusBar;
    [SerializeField] public StatusBar statusBarPrefab;
    [SerializeField] private RunManager runManagerPrefab;
    [SerializeField] public PlayerConfig playerConfig;
    [SerializeField] public Enemy enemy; //Tạm để test
    [SerializeField] public Button _continueButton;
    //[SerializeField] public StartRunButton startButtonPrefab;
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
        StartOrResume();
    }

    public void NewGame()
    {
        SceneManager.LoadScene("IntroScene");
    }
    public void StartNewRun()
    {
        ////Tạo run mới
        CreateStatusBar();
        RunManager.Instance.StartRun();
    }
    public void CreateStatusBar()
    {
        if (statusBar != null)
        {
            statusBar.gameObject.SetActive(true);
        }
        else
        {
            statusBar = Instantiate(statusBarPrefab);
            DontDestroyOnLoad(statusBar);
        }
    }
    //public void CreateStartButton()
    //{
    //    Vector2 buttonSpawn = new Vector2(0, 0);
    //    var button = Instantiate(startButtonPrefab, buttonSpawn,Quaternion.identity);
    //}
    public void StartOrResume()
    {
        if (_continueButton == null)
        {
            Debug.Log("Continue Button is null");
            return;
        }
        if (SaveManager.Instance.CurrentRun == null)
        {
            _continueButton.gameObject.SetActive(false);
            return;
        }
        else return;
    }
}
