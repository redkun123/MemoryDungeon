using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
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
    [SerializeField] public RoomDB roomDB;
    [SerializeField] RoomRest roomRest;
    [SerializeField] CardDB cardDB;
    [SerializeField] private RelicLibrary relicLibrary;
    [SerializeField] RewardPopup rewardPopupPrefab;
    public BattleSceneController battleSceneController { get; private set; }
    public BattleManager battleManager { get; private set; }
    public RunManager runManager { get; private set; }
    public StatusBar statusBar { get; private set; }
    public StoryManager storyManager { get; private set; }
    public RoomStory currentStory { get; private set; }
    public LobbyManager lobbyManager { get; private set; }
    public RewardGenerator rewardGenerator { get; private set; }
    public RelicManager relicManager { get; private set; }
    public Room currentRoom;
    public int currentFloor;
    [SerializeField] GameOverPopup gameOverPopupPrefab;
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
    public void CreatePLayer()
    {
        this.player = PlayerFactory.Create(GameManager.Instance.playerConfig);
    }
    public void StartBattle(EnemyConfig enemycf)
    {
        Debug.Log("Trying to start battle");
        currentEnemy = enemycf;
    }
    public void RegisterStatusUI()
    {
        StatusUIManager.Instance.RegisterCharacterStatus(battleSceneController);
    }
    public void StartStory(RoomStory chosenRoom)
    {
        Debug.Log("Trying to start story");
        currentStory = chosenRoom;
    }
    public void StartRun()
    {
        CreatePLayer();
        currentFloor = 0;
        roomManager = new RoomManager(roomDB);
        floorManager = new FloorManager();
        rewardGenerator = new RewardGenerator(cardDB, relicLibrary);
        relicManager = new(relicLibrary);
        Debug.Log("Floor Manager created");
        RegisterStatusBar();
        UpdateStatusBar();
        DeckUIManager.Instance.RegisterPlayer(player);
        RelicUIManager.Instance.RegisterRelic(relicManager);
        floorManager.Init(roomManager, currentFloor);
        floorManager.DefineCurrentRoom();
        CreateNewSave();
    }
    public void CreateNewSave()
    {
        SaveManager.Instance.CurrentRun = new RunSaveData
        {
            currentHP = player.currentHP,
            maxHP = player.maxHP,
            gold = player.gold,
            floor = 0,
            deckCardIds = player.GetDeckIDs(),
            visitedRoomIds = new List<string>(),
            currentRoomID = "0",
            relicList = relicManager.GetRelicData(),
        };
        SaveManager.Instance.SaveRun();
        SaveManager.Instance.SaveGame();
    }
    public void UpdateRunSave()
    {
        currentFloor = floorManager.floor;
        if (currentFloor <= 1)
        {
            return;
        }
        //currentRoom = roomManager.currentRoom;
        var run = SaveManager.Instance.CurrentRun;
        run.currentHP = player.currentHP;
        run.gold = player.gold;
        run.floor = currentFloor;
        run.deckCardIds = player.GetDeckIDs();
        run.currentRoomID = currentRoom.roomID;
        run.visitedRoomIds.Add(currentRoom.roomID);
        run.relicList = relicManager.GetRelicData();
        SaveManager.Instance.SaveRun();
    }
    public void ResumeRun()
    {
        var run = SaveManager.Instance.CurrentRun;
        if (run == null)
        {
            Debug.Log("No run to resume");
            return;
        }
        // rebuild player state
        var deck = LoadDeckFromSave(run);
        if (player == null)
        {
            CreatePLayer();
        }
        player.LoadFromRun(run, deck);
        roomManager = new RoomManager(roomDB);
        floorManager = new FloorManager();
        currentFloor = run.floor;
        floorManager.Init(roomManager, currentFloor);
        rewardGenerator = new RewardGenerator(cardDB, relicLibrary);
        relicManager = new(relicLibrary);
        for (int i = 0; i < run.relicList.Count; i++)
        {
            relicManager.AddRelicByID(run.relicList[i]); //tam thoi add lai relic
        }
        Debug.Log("Floor Manager created");
        GameManager.Instance.CreateStatusBar();
        RegisterStatusBar();
        UpdateStatusBar();
        DeckUIManager.Instance.RegisterPlayer(player);
        RelicUIManager.Instance.RegisterRelic(relicManager);
        LoadRoomFromSave(run);
    }
    public void LoadRoomFromSave(RunSaveData run)
    {
        currentRoom = roomDB.GetRoom(run.currentRoomID);
        Debug.Log($"Current room: {currentRoom.roomName}");
        roomManager.EnterChosenRoom(currentRoom);
    }
    public List<Card> LoadDeckFromSave(RunSaveData run)
    {
        foreach (var id in run.deckCardIds)
        {
            Debug.Log($"Saved ID: {id}");
        }
        var cardList = new List<Card>();
        for (int i = 0; i < run.deckCardIds.Count; i++)
        {
            var card = cardDB.GetCard(run.deckCardIds[i]);
            cardList.Add(card);
            Debug.Log($"Add card: {card.cardName}");
        }
        return cardList;
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
        floorManager.DefineCurrentRoom();
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
        Debug.Log($"Run Manager initiating room with ID {roomTempID}");
        roomManager.SetSelectedRoom(roomTempID);
        InitNextRoom();
    }
    public Room DisplayRoomName(int roomTempID)
    {
        return roomManager.randRoom[roomTempID];
    }
    public void GenerateRandomReward(RewardGenerator.RewardRank rank)
    {
        int rewardCount = 3;
        var rewards = rewardGenerator.RequestReward(rewardCount, rank);
        Debug.Log($"Reward Display Count: {rewards.Count}");
        DisplayRandomReward(rewards);
    }
    public void DisplayRandomReward(List<Reward> rewards)
    {
        RewardPopup rewardPopup = Instantiate(rewardPopupPrefab);
        rewardPopup.Init(rewards);
    }
    public void EndRun()
    {
        SaveData();
        Instantiate(gameOverPopupPrefab);
        gameOverPopupPrefab.Init();
        ResetRun();
    }
    public void LoadPrologue()
    {
        SceneManager.LoadScene("EndScene");
    }
    private void ResetRun()
    {
        SaveManager.Instance.ClearRun();
        player = null;
        enemy = null;
        roomManager = null;
        floorManager = null;
        statusBar = null;
        storyManager = null;
        currentStory = null;
        lobbyManager = null;
        rewardGenerator = null;
        relicManager.OnDestroy();
        statusBar.gameObject.SetActive(false);
        GameManager.Instance.StartOrResume();
    }
    public void SaveData()
    {
        var save = SaveManager.Instance;

        HashSet<string> completedSet = save.CurrentGame.ToHashSet();

        int killCount = 0;
        int newRooms = 0;

        foreach (var roomId in save.CurrentRun.visitedRoomIds)
        {
            Room room = roomDB.GetRoom(roomId);

            if (room.roomType == Room.RoomType.Battle)
            {
                killCount++;
            }

            if (completedSet.Add(roomId))
            {
                newRooms++;
            }
        }

        save.CurrentRun.killThisRun = killCount;
        save.CurrentRun.roomDiscoverThisRun = newRooms;

        save.CurrentGame.killTotal += killCount;
        save.CurrentGame.roomDiscoverTotal += newRooms;

        save.CurrentGame.FromHashSet(completedSet);

        save.SaveGame();

    }
}
