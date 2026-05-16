using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicUIManager : MonoBehaviour
{
    public static RelicUIManager Instance;
    private RelicManager relicManager;
    [SerializeField] private Transform content;
    [SerializeField] private RelicUI relicPrefab;
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
    public void RegisterRelic(RelicManager relicManager)
    {
        this.relicManager = relicManager;
        relicManager.OnReceiveRelic += RefreshInventory;
        RefreshInventory();
    }
    public void RefreshInventory()
    {
        var inventory = relicManager.currentRelicData;
        Debug.Log($"Refreshing relic list: {inventory}");
        // render
        foreach (Transform child in content)
        {
            Destroy(child.gameObject);
        }

        // spawn relic
        foreach (var relic in inventory)
        {
            var relicUI = Instantiate(relicPrefab, content);
            relicUI.SetupRelic(relic);
        }
    }
}
