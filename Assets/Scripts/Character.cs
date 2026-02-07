using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Character
{
    public string name;
    public int maxHP;
    public int currentHP;
    public bool isAlive;
    public event Action Dies;
    public event Action<int, int> OnHPChange;
    public void TakeDamage(int damage)
    {
        int oldHP = currentHP;
        currentHP -= damage;
        currentHP = Math.Max(currentHP, 0);
        if (currentHP != oldHP)
        {
            OnHPChange?.Invoke(currentHP, maxHP);
        }
        if (currentHP <= 0)
        {
            isAlive = false;
            Dies?.Invoke();
        }
        Debug.Log($"{name} lost {damage} HP");
    }

    public void GainGuard(int guard)
    {
        
    }
}
