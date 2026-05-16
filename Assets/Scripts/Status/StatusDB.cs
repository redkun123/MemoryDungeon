using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Status/Status Database")]
public class StatusDB : ScriptableObject
{
    [SerializeField] public List<StatusData> statuses;
    private List<StatusData> statusDatabase;
    public Dictionary<string, StatusData> _lookup;

    public void Init()
    {
        statusDatabase.Clear();
        for (int i = 0; i < statuses.Count; i++)
        {
            statusDatabase.Add(statuses[i]);
        }
        foreach (var status in statusDatabase)
        {
            Debug.Log($"Status Name: {status.name}");
        }
        if (_lookup != null)
        {
            _lookup.Clear();
        }
        _lookup = new Dictionary<string, StatusData>();
        foreach (var status in statusDatabase)
        {
            if (_lookup.ContainsKey(status.name))
            {
                Debug.LogError($"Duplicate status name: {status.name}");
                continue;
            }
            _lookup.Add(status.name, status);
        }
    }
    public StatusData GetStatus(string name)
    {
        Init();
        Debug.Log($"Looking for: {name}");
        foreach (var key in _lookup.Keys)
        {
            Debug.Log($"DB has: {key}");
        }
        if (_lookup.TryGetValue(name, out var status)) return status;
        statusDatabase.Clear();
        Debug.LogError($"Status not found: {name}");
        return null;
    }
}
