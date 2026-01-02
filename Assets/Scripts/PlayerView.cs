using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private Player player;

    [SerializeField] private HPBar hpBar;
    [SerializeField] private HPCount hpCount;

    public void Bind(Player player)
    {
        this.player = player;
        UpdateHP();
    }

    void UpdateHP()
    {
        player.OnHPChanged += hpBar.Set;
        player.OnHPChanged += hpCount.Set;
        hpBar.InitSet(player.currentHP, player.maxHP);
        hpCount.Set(player.currentHP, player.maxHP);
    }
}

