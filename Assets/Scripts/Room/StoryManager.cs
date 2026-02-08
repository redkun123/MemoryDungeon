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
    [SerializeField] GameObject optionPrefab;
    [SerializeField] Transform optionParent;
    public List<StoryResult> currentResults;
    void Awake()
    {
        currentResults = new List<StoryResult>();
    }
    public void Setup(RoomStory content)
    {
        title.text = content.storyTitle;
    //    LoadNodeContent(startingNodeID);
    //storyNodes;
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
    public void LoadNodeContent(string nodeID)
    {
        //    description.text = node.nodeDescription;
        //    storyImage = node.nodeImage;
        //    foreach (StoryButton s in node.optionButton)
        //    {
        //        //var btn = Instantiate(optionPrefab, optionParent);
        //        //btn.Init(s.option, this);
        //    }
    }
    public void InitNode(string nodeID)
    {

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
}
