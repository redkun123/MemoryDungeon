using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] Transform optionParent;
    [SerializeField] LobbyOptionButton optionButtonPrefab;
    private List<GameObject> spawnedOptions;
    private List<int> optionID;
    private float spacing = 500f;
    private void Awake()
    {
        RunManager.Instance.RegisterLobbyManager(this);
        optionID = new List<int>();
        spawnedOptions = new();
        SpawnOptionButton(RunManager.Instance.roomManager.randRoom.Count);
        UpdateOptionVisual();
    }
    private void OnDestroy()
    {
        spawnedOptions.Clear();
        optionID.Clear();
    }
    //Spawn nut option va gan ID option cho cac nut
    public void SpawnOptionButton(int buttonCount)
    {
        for (int i = 0; i < buttonCount; i++)
        {
            optionID.Add(i);
        }
        foreach (var id in optionID)
        {
            var btn = Instantiate(optionButtonPrefab, optionParent);
            btn.Init(id);
            spawnedOptions.Add(btn.gameObject);
        }
    }
    private void UpdateOptionVisual()
    {
        int optionCount = spawnedOptions.Count;
        for (int i = 0; i < optionCount; i++)
        {
            float horizontalOffset = (spacing * (i - (optionCount - 1) / 2f));
            spawnedOptions[i].transform.localPosition = new Vector3(horizontalOffset, 0, 0);
        }
    }
}
