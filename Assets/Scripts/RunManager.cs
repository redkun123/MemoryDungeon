using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunManager : MonoBehaviour
{
    public Player player;
    public RoomManager roomManager { get; private set; }
    [SerializeField] RoomDB roomDB;
    public BattleSceneController battleSceneController;
    public RunData run { get; private set; }
    public RunManager runManager { get; private set; }

    public void Init(GameData data)
    {
        run = new RunData(data);
        StartRun();
    }
    public void CreatePLayer()
    {
        this.player = PlayerFactory.Create(GameManager.Instance.playerConfig);
    }
    public void StartBattle(Enemy enemy)
    {
        BattleManager battleManager = new BattleManager();
        battleManager.StartBattle(player);
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
}
