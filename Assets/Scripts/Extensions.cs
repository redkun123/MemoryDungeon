using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class Extensions
{
    public static void Shuffle<T>(this List<T> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            T temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
    public static string Concatenate(this List<string> list)
    {
        if (list.Count == 0) return string.Empty;
        return string.Join("\n", list.Select(x => $"{x}."));
    }
    public static int PayGold(int currentGold, int requiredGold)
    {
        if (currentGold >= requiredGold)
        {
            currentGold = currentGold - requiredGold;
            return currentGold;
        }
        else
        {
            Debug.Log("Not enough Gold");
            return currentGold;
        }
    }
}
