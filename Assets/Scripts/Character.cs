using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Character
{
    public string Name;
    public int MaxHP;
    public int CurrentHP;
    public bool IsAlive;
    public event Action<int, int> OnHPChanged;
    public void TakeDamage(int damage)
    {
        int oldHP = CurrentHP;
        CurrentHP -= damage;
        CurrentHP = Math.Max(CurrentHP, 0);
        if (CurrentHP != oldHP)
        {
            OnHPChanged?.Invoke(CurrentHP, MaxHP);
        }
    }
}
