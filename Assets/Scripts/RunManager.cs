using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(1)]
public class RunManager : MonoBehaviour
{
    public static RunManager Instance;
    public Player player;
    public Enemy enemy;
    public EnemyConfig currentEnemy;
    public RoomManager roomManager { get; private set; }
    [SerializeField] RoomDB roomDB;
    public BattleSceneController battleSceneController { get; private set; }
    public BattleManager battleManager { get; private set; }
    public RunData run { get; private set; }
    public RunManager runManager { get; private set; }

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
    public void Init(GameData data)
    {
        run = new RunData(data);
        StartRun();
    }
    public void CreatePLayer()
    {
        this.player = PlayerFactory.Create(GameManager.Instance.playerConfig);
    }
    public void StartBattle(EnemyConfig enemycf)
    {
        //Debug.Log("Trying to start battle");
        //battleManager.StartBattle(player, enemycf);
        currentEnemy = enemycf;
    }
    public void StartRun()
    {
        CreatePLayer();
        RoomManager roomManager = new RoomManager(roomDB);
        FloorManager floorManager = new FloorManager();
        Debug.Log("Floor Manager created");
        //Load Floor 0 để bắt đầu game
        floorManager.Init(roomManager);
    }
    public void End()
    {
        Destroy(gameObject);
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
}
