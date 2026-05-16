using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusUIManager : MonoBehaviour
{
    public static StatusUIManager Instance;
    private StatusManager playerSM;
    [SerializeField] private Transform content;
    [SerializeField] private StatusUI statusPrefab;
    [SerializeField] private StatusDB statusDB;
    [SerializeField] private Character owner;
    private Dictionary<StatusData, string> statusList;
    private Player player;
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
    public void RegisterPlayerStatus(Player player)
    {
        this.player = player;
        playerSM = player.statusManager;
        playerSM.OnStatusListChange += RefreshPlayerStatusList;
        RefreshPlayerStatusList();
    }
    public void RefreshPlayerStatusList()
    {
        if (statusList == null)
        {
            statusList = new Dictionary<StatusData, string>();
        }
        else
        {
            statusList.Clear();
        }
        var nameList = playerSM.GetAll();
        foreach (var item in nameList)
        {
            var statusData = statusDB.GetStatus(item.Key);
            statusList.Add(statusData, item.Value);
        }
        Debug.Log($"Refreshing status list: {statusList}");
        // render
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        // spawn status icon
        foreach (var status in statusList)
        {
            var statusUI = Instantiate(statusPrefab, content);
            statusUI.SetupStatus(status.Key, status.Value);
        }
    }
}
