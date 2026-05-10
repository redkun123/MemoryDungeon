using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
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
    public event Action<int> OnAttacked;
    public event Action<int> OnModifyGuard;
    public event Action OnLostGuard;
    public int currentGuard;
    public List<Status> status;
    public StatusManager statusManager;
    public void TakeDamage(int damage)
    {
        //Nếu bị attack thì trừ Guard trước, còn thừa bao nhiêu thì mới trừ HP
        OnAttacked?.Invoke(damage);
        int calcDamage = damage;
        int oldHP = currentHP;
        if (currentGuard > 0)
        {
            calcDamage = LostGuard(calcDamage);
        }
        currentHP -= calcDamage;
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
        Debug.Log($"{name} lost {calcDamage} HP");
    }

    public void GainGuard(int guard)
    {
        //Guard = máu giả
        //Gain Guard = tính vào 1 thanh/icon riêng
        
        var oldGuard = currentGuard;
        currentGuard += guard;
        currentGuard = Math.Min(currentGuard, 999);
        if (currentGuard != oldGuard)
        {
            OnModifyGuard?.Invoke(currentGuard);
        }
    }
    public int LostGuard(int dmg)
    {
        int oldGuard;
        oldGuard = currentGuard;
        if (currentGuard >= dmg)
        { 
            currentGuard -= dmg;
            dmg = 0;
        }
        else
        {
            dmg -= currentGuard;
            currentGuard = 0;
        }
        currentGuard = Mathf.Max(currentGuard, 0);
        dmg = Mathf.Max(dmg, 0);
        Debug.Log($"{name} lost {oldGuard - currentGuard} Guards\n{name} has {currentGuard} Guards left.");
        if (currentGuard != oldGuard)
        {
            OnModifyGuard?.Invoke(currentGuard);
        }
        if (currentGuard == 0)
        {
            OnLostGuard?.Invoke();
        }
        return dmg;
    }
    public void ClearGuard()
    {
        if (currentGuard != 0)
        {
            currentGuard = 0;
            OnLostGuard?.Invoke();
        }
        else return;
    }
    public void RestoreHP(int amount) //Hồi máu
    {
        int oldHP = currentHP;
        currentHP += amount;
        currentHP = Math.Min(currentHP, maxHP);
        if (currentHP != oldHP)
        {
            OnHPChange?.Invoke(currentHP,maxHP);
            Debug.Log($"{name} healed. Current HP: {currentHP}");
        }
        else
        {
            Debug.Log($"{name}'s HP is full!");
        }
    }
}
