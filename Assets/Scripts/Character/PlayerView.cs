using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private Player player;

    [SerializeField] private HPBar hpBar;
    [SerializeField] private HPCount hpCount;
    [SerializeField] private GuardCount guardCount;
    [SerializeField] private Transform statusArea;

    public void Bind(Player player)
    {
        this.player = player;
        UpdateHP();
        RegisterGuard();
    }

    void UpdateHP()
    {
        player.OnHPChange += hpBar.Set;
        player.OnHPChange += hpCount.Set;
        hpBar.InitSet(player.currentHP, player.maxHP);
        hpCount.Set(player.currentHP, player.maxHP);
    }
    void RegisterGuard()
    {
        player.OnModifyGuard += guardCount.ModifyGuard;
        player.OnLostGuard += guardCount.LostGuard;
    }
}

