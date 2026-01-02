using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Character
{
    public string Name;
    public int maxHP;
    public int currentHP;
    public bool isAlive;
    public event Action<int, int> OnHPChanged;
    public void TakeDamage(int damage)
    {
        int oldHP = currentHP;
        currentHP -= damage;
        currentHP = Math.Max(currentHP, 0);
        if (currentHP != oldHP)
        {
            OnHPChanged?.Invoke(currentHP, maxHP);
        }
    }
}
