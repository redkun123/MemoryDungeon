using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HPCount : MonoBehaviour
{
    public TextMeshProUGUI hpCount;
    public void Set(int currentHP, int maxHP)
    {
        hpCount.text = $"{currentHP}/{maxHP}";
    }
}
