using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(1)]
public class RunManager : MonoBehaviour
{
    public static RunManager Instance;
    public int temp;
    public Player player;
    public Enemy enemy;
    public EnemyConfig currentEnemy;
    public RoomManager roomManager { get; private set; }
    public FloorManager floorManager { get; private set; }
    [SerializeField] RoomDB roomDB;
    [SerializeField] RoomRest roomRest;
    [SerializeField] CardDB cardDB;
    [SerializeField] private RelicLibrary relicLibrary;
    [SerializeField] RewardPopup rewardPopupPrefab;
    public BattleSceneController battleSceneController { get; private set; }
    public BattleManager battleManager { get; private set; }
    public SaveData run { get; private set; }
    public RunManager runManager { get; private set; }
    public StatusBar statusBar { get; private set; }
    public StoryManager storyManager { get; private set; }
    public RoomStory currentStory { get; private set; }
    public LobbyManager lobbyManager { get; private set; }
    public RewardGenerator rewardGenerator { get; private set; }
    public RelicManager relicManager { get; private set; }
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
    //public void Init(SaveSystem data)
    //{
    //    run = new SaveData(data);
    //    StartRun();
    //}
    public void CreatePLayer()
    {
        this.player = PlayerFactory.Create(GameManager.Instance.playerConfig);
    }
    public void StartBattle(EnemyConfig enemycf)
    {
        Debug.Log("Trying to start battle");
        currentEnemy = enemycf;
    }
    public void StartStory(RoomStory chosenRoom)
    {
        Debug.Log("Trying to start story");
        currentStory = chosenRoom;
    }
    public void StartRun()
    {
        CreatePLayer();
        roomManager = new RoomManager(roomDB);
        floorManager = new FloorManager();
        rewardGenerator = new RewardGenerator(cardDB);
        relicManager = new(relicLibrary);
        Debug.Log("Floor Manager created");
        RegisterStatusBar();
        UpdateStatusBar();
        DeckUIManager.Instance.RegisterPlayer(player);
        //roomManager.RoomCompleted += this.RoomComplete;
        //Load Floor 0 để bắt đầu game
        floorManager.Init(roomManager);
    }
    public void End()
    {
        Destroy(gameObject);
    }
    public void RegisterStatusBar(StatusBar sB)
    {
        statusBar = sB;
    }
    public void UpdateStatusBar()
    {
        int currentFloor = floorManager.floor;
        statusBar.UpdateStatus(player, currentFloor);
    }
    public void UpdateStatusHP(int oldHP, int newHP)
    {
        UpdateStatusBar();
    }
    public void UpdateStatusGold(int gold)
    {
        UpdateStatusBar();
    }
    public void UpdateStatusDeck(List<Card> deck)
    {
        UpdateStatusBar();
    }
    public void RegisterStatusBar()
    {
        player.OnHPChange += UpdateStatusHP;
        player.OnGoldChange += UpdateStatusGold;
        player.OnTrueDeckChange += UpdateStatusDeck;
    }
    public void RegisterBattleScene(BattleSceneController controller)
    {
        battleSceneController = controller;
        Debug.Log("Battle Scene Controller registered");
    }

    public void UnregisterBattleScene(BattleSceneController controller)
    {
        if (battleSceneController == controller)
            battleSceneController = null;
    }
    public void RegisterBattleManager(BattleManager bm)
    {
        battleManager = bm;
        Debug.Log("Battle Manager registered");
    }

    public void UnregisterBattleManager(BattleManager bm)
    {
        if (battleManager == bm)
            battleManager = null;
    }
    public void RegisterStoryManager(StoryManager sm)
    {
        storyManager = sm;
        Debug.Log("Story Manager registered");
    }

    public void UnregisterStoryManager(StoryManager sm)
    {
        if (storyManager == sm)
            storyManager = null;
    }
    public void RegisterLobbyManager(LobbyManager lm)
    {
        lobbyManager = lm;
        Debug.Log("Lobby Manager registered");
    }

    public void UnregisterLobbyManager(LobbyManager lm)
    {
        if (lobbyManager == lm)
            lobbyManager = null;
    }
    //public void RegisterRewardGenerator(RewardGenerator rg)
    //{
    //    rewardGenerator = rg;
    //    Debug.Log("Reward Generator registered");
    //}

    //public void UnregisterRewardGenerator(RewardGenerator rg)
    //{
    //    if (rewardGenerator == rg)
    //        rewardGenerator = null;
    //}
    public void GetEnemy()
    {
        enemy = battleManager.enemy;
    }

    public void UnregisterEnemy(Enemy e, EnemyConfig ef)
    {
        if (enemy == e) enemy = null;
        if (currentEnemy == ef) currentEnemy = null;
        Debug.Log("Enemy unregistered");
    }
    public void RoomComplete()
    {
        //roomManager.RoomComplete();
        roomManager.SpawnRandomRoom();
        SceneManager.LoadScene("LobbyScene");
    }
    public void InitNextRoom()
    {
        Debug.Log("Loading next room");
        floorManager.RoomOption();
    }
    public void Rest()
    {
        Extensions.PayGold(player.gold, roomRest.healCost);
        int healHP = Convert.ToInt32(Math.Round((player.currentHP * roomRest.healAmount)));
        player.RestoreHP(healHP);
        //RoomCompleted?.Invoke(this);
        RoomComplete();
    }
    public void BindRandomRoom(int roomTempID)
    {
        temp = roomTempID;
        Debug.Log("Room Binded");
        InitNextRoom();
    }
    public string DisplayRoomName(int roomTempID)
    {
        return roomManager.randRoom[roomTempID].roomName;
    }
    public void GenerateRandomReward(RewardGenerator.RewardRank rank)
    {
        int rewardCount = 3;
        List<Reward> rewards = new List<Reward>();
        rewards = rewardGenerator.RequestReward(rewardCount, rank);
        Debug.Log($"Reward Display Count: {rewards.Count}");
        DisplayRandomReward(rewards);
    }
    public void DisplayRandomReward(List<Reward> rewards)
    {
        RewardPopup rewardPopup = Instantiate(rewardPopupPrefab);
        rewardPopup.Init(rewards);
    }
}
