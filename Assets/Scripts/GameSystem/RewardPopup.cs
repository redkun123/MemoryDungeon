using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RewardPopup : MonoBehaviour
{
    [SerializeField] RandomReward randomRewardPrefab;
    [SerializeField] Transform rewardParent;
    private int rewardCount;
    private List<GameObject> spawnedReward;
    private float spacing = 500f;
    [SerializeField] TextMeshProUGUI title;
    public void Init(List<Reward> currentReward)
    {
        title.text = "Choose your reward";
        int rewardCount = currentReward.Count;
        spawnedReward = new List<GameObject>();
        //Spawn các nút phần thưởng
        for (int i = 0; i < rewardCount; i++)
        {
            var btn = Instantiate(randomRewardPrefab, rewardParent);
            btn.Init(currentReward[i], RunManager.Instance.rewardGenerator);
            spawnedReward.Add(btn.gameObject);
        }
        UpdateRewardVisual();
    }
    private void UpdateRewardVisual()
    {
        int rewardCount = spawnedReward.Count;
        for (int i = 0; i < rewardCount; i++)
        {
            float horizontalOffset = (spacing * (i - (rewardCount - 1) / 2f));
            spawnedReward[i].transform.localPosition = new Vector3(horizontalOffset, 0, 0);
        }
    }
    private void OnDestroy()
    {
        spawnedReward.Clear();
    }
}
