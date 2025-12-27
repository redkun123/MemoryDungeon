using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private Player player;

    [SerializeField] private HPBar hpBar;

    public void Bind(Player player)
    {
        this.player = player;
        UpdateHP();
    }

    void UpdateHP()
    {
        player.OnHPChanged += hpBar.Set;
        hpBar.Set(player.CurrentHP, player.MaxHP);
    }
}

