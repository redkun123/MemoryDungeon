using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicContext
{
    public Player player => battleManager.player;
    public Enemy enemy => battleManager.enemy;
    public BattleManager battleManager;

    public void Init(BattleManager battleManager)
    {
        this.battleManager = battleManager;
    }
}
