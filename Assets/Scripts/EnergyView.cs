using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyView : MonoBehaviour
{
    private Player player;

    [SerializeField] private EnergyBar energyBar;
    [SerializeField] private EnergyCount energyCount;

    public void Bind(Player player)
    {
        this.player = player;
        UpdateHP();
    }

    void UpdateHP()
    {
        player.OnHPChanged += energyBar.Set;
        player.OnHPChanged += energyCount.Set;
        energyBar.InitSet(player.CurrentHP, player.MaxHP);
        energyCount.Set(player.CurrentHP, player.MaxHP);
    }
}

