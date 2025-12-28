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
        //hpBar.value = currentHP;
        hpBar.value = Mathf.Clamp(currentHP, 0, maxHP);
        Debug.Log($"HPBar.max = {hpBar.maxValue}");
        Debug.Log($"HPBar.min = {hpBar.minValue}");
        Debug.Log($"HPBar.value = {hpBar.value}");
    }    
}
