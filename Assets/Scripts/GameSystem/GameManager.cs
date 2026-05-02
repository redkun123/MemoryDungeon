using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor;

[DefaultExecutionOrder(1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public StatusBar statusBar;
    [SerializeField] public StatusBar statusBarPrefab;
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
        if (statusBar != null)
        {
            statusBar.gameObject.SetActive(true);
        }
        else
        {
            statusBar = Instantiate(statusBarPrefab);
            DontDestroyOnLoad(statusBar);
        }
        RunManager.Instance.StartRun();
    }
    public void EndRun()
    {
        //Destroy(_runManager.gameObject);
    }
    public void Save()
    {
        SaveData data = new SaveData();
        data.gold = RunManager.Instance.player.gold;
        data.currentHP = RunManager.Instance.player.currentHP;
        data.maxHP = RunManager.Instance.player.maxHP;
        data.currentRoomId = RunManager.Instance.roomManager.currentRoom.roomID;
        data.completedRooms = RunManager.Instance.roomManager.usedRoomID;

        SaveSystem.SaveGame(data);
    }

    public void Load()
    {
        SaveData data = SaveSystem.LoadGame();
        if (data == null) return;
        //RunManager.Instance.player.Init(data.currentHP, data.maxHP, data.gold);
        RunManager.Instance.roomManager.usedRoomID = data.completedRooms;
        RunManager.Instance.roomManager.ShowCurrentRoom(data.currentRoomId);
    }
}
