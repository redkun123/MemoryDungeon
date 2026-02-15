using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class StoryManager : MonoBehaviour
{
    [SerializeField] Image storyImage;
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI description;
    //[SerializeField] GameObject optionPrefab;
    [SerializeField] Transform optionParent;
    public List<StoryResult> currentResults;
    public Dictionary<string, StoryNode> nodeMap;
    public StoryNode currentNode;
    public RoomStory currentRoom;
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
                    break;
                case StoryResult.ResultType.ModifyHP:
                    break;
                case StoryResult.ResultType.ModifyCard:
                    break;
                case StoryResult.ResultType.ModifyRelic:
                    break;
                case StoryResult.ResultType.Leave:
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
        //foreach (StoryButton s in currentNode.optionButton)
        //{
        //    //var btn = Instantiate(optionPrefab, optionParent);
        //    //btn.Init(s.option, this);
        //}
    }
    void InitNode(string nodeId)
    {
        currentNode = GetNode(nodeId);
        if (currentNode == null) return;
        LoadNodeContent(currentNode);
    }
    public void ModifyGold()
    {

    }
    public void ModifyHP()
    {

    }
    public void ModifyCard()
    {

    }
    public void ModifyRelic()
    {

    }
    public void Leave()
    {

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
}
