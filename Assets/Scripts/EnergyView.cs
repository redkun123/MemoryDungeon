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
        UpdateEnergy();
    }

    void UpdateEnergy()
    {
        player.OnEnergyChanged += energyBar.Set;
        player.OnEnergyChanged += energyCount.Set;
        energyBar.InitSet(player.currentEnergy, player.maxEnergy);
        energyCount.Set(player.currentEnergy, player.maxEnergy);
        Debug.Log($"Energy view: {player.currentEnergy} / {player.maxEnergy}");
    }
}

