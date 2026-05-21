using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusUIManager : MonoBehaviour
{
    public static StatusUIManager Instance;
    private StatusManager playerSM;
    private StatusManager enemySM;
    [SerializeField] private StatusUI statusPrefab;
    [SerializeField] public StatusDB statusDB;
    private Dictionary<StatusData, string> playerStatusList;
    private Dictionary<StatusData, string> enemyStatusList;
    private Player player;
    private Enemy enemy;
    private PlayerView playerView;
    private EnemyView enemyView;

    [SerializeField]
    private StatusPopupUI popupPrefab;

    //[SerializeField]
    //private Canvas battleCanvas;
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
    public void RegisterCharacterStatus(BattleSceneController controller)
    {
        this.player = controller.Player;
        this.playerView = controller.playerView;
        playerSM = player.statusManager;
        playerSM.OnStatusListChange += RefreshPlayerStatusList;
        this.enemy = controller.Enemy;
        this.enemyView = controller.enemyView;
        enemySM = enemy.statusManager;
        enemySM.OnStatusListChange += RefreshEnemyStatusList;
        RefreshPlayerStatusList();
        RefreshEnemyStatusList();
    }
    public void RefreshPlayerStatusList()
    {
        if (playerStatusList == null)
        {
            playerStatusList = new Dictionary<StatusData, string>();
        }
        else
        {
            playerStatusList.Clear();
        }
        var nameList = playerSM.GetAll();
        foreach (var item in nameList)
        {
            var statusData = statusDB.GetStatus(item.Key);
            playerStatusList.Add(statusData, item.Value);
        }
        Debug.Log($"Refreshing status list: {playerStatusList}");
        var content = playerView.statusArea;
        // render
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
        // spawn status icon
        foreach (var status in playerStatusList)
        {
            var statusUI = Instantiate(statusPrefab, content);
            statusUI.SetupStatus(status.Key, status.Value);
        }
    }
    public void RefreshEnemyStatusList()
    {
        if (enemyStatusList == null)
        {
            enemyStatusList = new Dictionary<StatusData, string>();
        }
        else
        {
            enemyStatusList.Clear();
        }
        var nameList = enemySM.GetAll();
        foreach (var item in nameList)
        {
            var statusData = statusDB.GetStatus(item.Key);
            enemyStatusList.Add(statusData, item.Value);
        }
        Debug.Log($"Refreshing status list: {enemyStatusList}");
        var content = enemyView.statusArea;
        // render
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }
        // spawn status icon
        foreach (var status in enemyStatusList)
        {
            var statusUI = Instantiate(statusPrefab, content);
            statusUI.SetupStatus(status.Key, status.Value);
        }
    }
    public IEnumerator PlayStatusPopup(Character owner, StatusData data)
    {
        Transform anchor = null;

        if (owner == player)
        {
            anchor = playerView.statusPopupAnchor;
        }
        //else
        //{
        //    anchor = enemyView.statusPopupAnchor;
        //}

        var popup = Instantiate(popupPrefab, anchor);
        Debug.Log("Status UI created)");
        yield return popup.Play(data.icon, anchor);
    }
}