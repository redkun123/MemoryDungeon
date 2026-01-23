using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunManager : MonoBehaviour
{
    public Player player;
    [SerializeField] PlayerConfig playerConfig;
    [SerializeField] BattleManager battleManager;
    public BattleSceneController battleSceneController;
    public void CreatePLayer()
    {
        this.player = PlayerFactory.Create(playerConfig);
    }
    public void StartBattle(Enemy enemy)
    {
        CreatePLayer();
        battleManager.StartBattle(this.player);
    }
}
