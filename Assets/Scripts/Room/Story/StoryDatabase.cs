using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class StoryDatabase : MonoBehaviour
{
    public Dictionary<string, StoryNode> nodeMap;

    [SerializeField] private string nodeCSVUrl;
    [SerializeField] private string optionCSVUrl;

    private void Start()
    {
        StartCoroutine(LoadStoryData());
    }

    IEnumerator LoadStoryData()
    {
        yield return StartCoroutine(DownloadNodeCSV());
        yield return StartCoroutine(DownloadOptionCSV());

        Debug.Log("Story Loaded");
    }

    IEnumerator DownloadNodeCSV()
    {
        UnityWebRequest req = UnityWebRequest.Get(nodeCSVUrl);

        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(req.error);
            yield break;
        }

        ParseNodeCSV(req.downloadHandler.text);
    }

    IEnumerator DownloadOptionCSV()
    {
        UnityWebRequest req = UnityWebRequest.Get(optionCSVUrl);

        yield return req.SendWebRequest();

        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError(req.error);
            yield break;
        }

        ParseOptionCSV(req.downloadHandler.text);
    }
    void ParseNodeCSV(string csv)
    {
        string[] lines = csv.Split('\n');

        for (int i = 1; i < lines.Length; i++)
        {
            string[] cols = lines[i].Split(',');

            StoryNode node = ScriptableObject.CreateInstance<StoryNode>();

            node.nodeID = cols[0];
            node.nodeDescription = cols[1];

            node.options = new List<StoryOption>();

            nodeMap.Add(node.nodeID, node);
        }
    }
    void ParseOptionCSV(string csv)
    {
        string[] lines = csv.Split('\n');

        for (int i = 1; i < lines.Length; i++)
        {
            string[] cols = lines[i].Split(',');

            string fromNode = cols[0];

            StoryOption option = ScriptableObject.CreateInstance<StoryOption>();

            option.optionDescription = cols[1];

            StoryResult result = ScriptableObject.CreateInstance<StoryResult>();

            result.type = Enum.Parse<StoryResult.ResultType>(cols[2]);
            result.value = cols[3];

            option.results = new List<StoryResult>()
        {
            result
        };

            nodeMap[fromNode].options.Add(option);
        }
    }
}