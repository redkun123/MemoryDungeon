using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyIntention : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI intention;
    public void ShowIntention(Enemy enemy)
    {
        if (enemy == null) return;
        if (enemy.moveSet == null) return;
        if (enemy.moveSet.Count == 0) return;
        if (enemy.turnCount < 0 || enemy.turnCount >= enemy.moveSet.Count) return;
        List<string> intentionList = new List<string>();
        foreach (var effect in enemy.moveSet[enemy.turnCount].cardEffect)
        {
            intentionList.Add(effect.effectDescription);
        }
        intention.text = Extensions.Concatenate(intentionList);
    }
}
