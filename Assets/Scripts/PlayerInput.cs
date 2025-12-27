using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public BattleManager battleManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            battleManager.EndPlayerTurn();
        }
    }
}
