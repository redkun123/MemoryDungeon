using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public List<Card> moveSet;
    public void Attack(Player player)
    {
        player.TakeDamage(10);
        Debug.Log("Enemy attacked!");
    }
    //public Enemy()
    //{
    //    name = "Drago";
    //    maxHP = 200;
    //    currentHP = maxHP;
    //    isAlive = true;
    //}
}
