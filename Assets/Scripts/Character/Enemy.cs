using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : Character
{
    public List<Card> moveSet;
    public int turnCount;
    public Image avatar;
    //public void Attack(Player player)
    //{
    //    player.TakeDamage(10);
    //    Debug.Log("Enemy attacked!");
    //}
    //public Enemy()
    //{
    //    name = "Drago";
    //    maxHP = 200;
    //    currentHP = maxHP;
    //    isAlive = true;
    //}
    public IEnumerator PlayHitAnimation()
    {
        yield return new WaitForSeconds(0.25f);
    }
}
