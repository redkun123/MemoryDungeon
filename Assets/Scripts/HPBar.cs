using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    [SerializeField] private Slider hpBar;
    [SerializeField] Transform fill;

    public void Set(int currentHP, int maxHP)
    {
        hpBar.maxValue = maxHP;
        hpBar.minValue = 0;
        if (maxHP <= 0) return;
        hpBar.value = (float)currentHP / maxHP;
        fill.localScale = new Vector3(hpBar.value, 1f, 1f);
    }    
}
