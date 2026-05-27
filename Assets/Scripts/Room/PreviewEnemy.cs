using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewEnemy : MonoBehaviour
{
    [SerializeField] public Image monsterIcon;
    [SerializeField] public Image starPrefab;
    [SerializeField] Transform starParent;
    public int starCount;

    public void Init(EnemyConfig enemy)
    {
        starCount = enemy.starCount;
        monsterIcon.sprite = enemy.enemyAvatar;
        SpawnStar();
    }
    private void SpawnStar()
    {
        foreach (Transform child in starParent)
        {
            Destroy(child.gameObject);
        }

        // Spawn new stars
        for (int i = 0; i < starCount; i++)
        {
            Instantiate(starPrefab, starParent);
        }
    }
}
