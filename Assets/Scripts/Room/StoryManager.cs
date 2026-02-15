using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
public class StoryManager : MonoBehaviour
{
    [SerializeField] Image storyImage;
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] StoryButton optionPrefab;
    [SerializeField] Transform optionParent;
    public float spacing = 80f;
    public List<StoryResult> currentResults;
    public Dictionary<string, StoryNode> nodeMap;
    public StoryNode currentNode;
    public RoomStory currentRoom;
    public List<GameObject> spawnedOptions;
    void Awake()
    {
        RunManager.Instance.RegisterStoryManager(this);
        currentRoom = RunManager.Instance.currentStory;
        Setup(currentRoom);
    }
    public void Setup(RoomStory content)
    {
        //Setup các Story Node
        nodeMap = new Dictionary<string, StoryNode>();
        foreach (var node in currentRoom.storyNodes)
        {
            if (nodeMap.ContainsKey(node.nodeID))
            {
                Debug.LogError($"Duplicate node id: {node.nodeID}");
                continue;
            }
            nodeMap.Add(node.nodeID, node);
        }
        //currentResults = new List<StoryResult>();
        currentNode = GetNode(currentRoom.startingNodeID);
        LoadNodeContent(currentNode);
    }
    public void ExecuteChosenOption(StoryOption storyOption)
    {
        foreach (StoryResult sR in storyOption.results)
        {
            currentResults.Add(sR);
        }
        for (int i = 0; i < currentResults.Count; i++)
        {
            switch (currentResults[i].type)
            {
                case StoryResult.ResultType.None:
                    Debug.Log($"Result {i} don't have a type");
                    break;
                case StoryResult.ResultType.InitNode:
                    InitNode(currentResults[i].value);
                    break;
                case StoryResult.ResultType.ModifyGold:
                    int tempG;
                    if (int.TryParse(currentResults[i].value, out tempG))
                    {
                        tempG = int.Parse(currentResults[i].value);
                    }
                    else
                    {
                        Console.WriteLine("So Gold can mod can la so nguyen");
                    }
                    ModifyGold(tempG);
                    break;
                case StoryResult.ResultType.ModifyHP:
                    int tempH;
                    if (int.TryParse(currentResults[i].value, out tempH))
                    {
                        tempH = int.Parse(currentResults[i].value);
                    }
                    else
                    {
                        Console.WriteLine("So HP can mod can la so nguyen");
                    }
                    ModifyHP(tempH);
                    break;
                case StoryResult.ResultType.ModifyCard:
                    ModifyCard();
                    break;
                case StoryResult.ResultType.ModifyRelic:
                    ModifyRelic();
                    break;
                case StoryResult.ResultType.Leave:
                    Leave();
                    break;
                default:
                    Debug.Log($"Result {i} has unrecognizable type");
                    break;
            }
        }
        currentResults.Clear();
    }
    public void LoadNodeContent(StoryNode currentNode)
    {
        title.text = currentRoom.storyTitle;
        description.text = currentNode.nodeDescription;
        storyImage.sprite = currentNode.nodeImage;
        //currentResults = currentNode.optionButton;
        foreach (var option in currentNode.options)
        {
            var btn = Instantiate(optionPrefab, optionParent);
            btn.Init(option, this);
            spawnedOptions.Add(btn.gameObject);
        }
        UpdateOptionVisual();
    }
    void InitNode(string nodeId)
    {
        currentNode = GetNode(nodeId);
        if (currentNode == null) return;
        if (spawnedOptions != null)
        {
            foreach(var option in spawnedOptions)
            {
                Destroy(option.gameObject);
            }
            spawnedOptions.Clear();
        }
        LoadNodeContent(currentNode);
    }
    public void ModifyGold(int tempG)
    {
        RunManager.Instance.player.ModifyGold(tempG);
    }
    public void ModifyHP(int tempH)
    {
        if (tempH >= 0)
        {
            RunManager.Instance.player.RestoreHP(tempH);
        }
        else if (tempH < 0)
        {
            RunManager.Instance.player.TakeDamage(tempH);
        }
    }
    public void ModifyCard()
    {

    }
    public void ModifyRelic()
    {

    }
    public void Leave()
    {
        RunManager.Instance.RoomComplete();
    }
    public StoryNode GetNode(string nodeId)
    {
        if (nodeMap.TryGetValue(nodeId, out var node))
            return node;

        Debug.LogError($"Node id not found: {nodeId}");
        return null;
    }
    private void OnDestroy()
    {
        if (RunManager.Instance != null)
            RunManager.Instance.UnregisterStoryManager(this);
    }
    private void UpdateOptionVisual()
    {
        int optionCount = spawnedOptions.Count;
        for (int i = 0; i < optionCount; i++)
        {
            float verticalOffset = (spacing * (i - (optionCount - 1) / 2f));
            spawnedOptions[i].transform.localPosition = new Vector3(0, verticalOffset, 0);
        }
    }
}
