using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunManager : MonoBehaviour
{
    public Player player;
    PlayerConfig playerConfig;
    public BattleManager battleManager;
    public void CreatePLayer()
    {
        this.player = PlayerFactory.Create(playerConfig);
    }
    public void StartBattle()
    {
        battleManager = new BattleManager();
        CreatePLayer();
        battleManager.StartBattle(this.player);
    }
}
