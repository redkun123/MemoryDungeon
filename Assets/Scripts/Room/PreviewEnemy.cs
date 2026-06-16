using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewEnemy : MonoBehaviour
{
    [SerializeField] public Image monsterIcon;
    [SerializeField] public Image starPrefab;
    [SerializeField] Transform enemyStarParent;
    [SerializeField] Transform rewardStarParent;
    public int starCount;

    public void Init(EnemyConfig enemy)
    {
        starCount = enemy.starCount;
        monsterIcon.sprite = enemy.enemyAvatar;
        SpawnStar();
    }
    private void SpawnStar()
    {
        foreach (Transform child in enemyStarParent)
        {
            Destroy(child.gameObject);
        }

        // Spawn new stars
        for (int i = 0; i < starCount; i++)
        {
            Instantiate(starPrefab, enemyStarParent);
        }
        var rewardStar = Random.Range(starCount - 1, starCount + 1);
        if (rewardStar < 1)
        {
            rewardStar = 1;
        }
        foreach (Transform child in rewardStarParent)
        {
            Destroy(child.gameObject);
        }
        // Spawn new stars
        for (int j = 0; j < rewardStar; j++)
        {
            Instantiate(starPrefab, rewardStarParent);
        }
    }
}
