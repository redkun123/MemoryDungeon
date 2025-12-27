using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnButton : MonoBehaviour
{
    public BattleManager battleManager;

    public void OnClickEndTurn()
    {
        battleManager.EndPlayerTurn();
    }
}
