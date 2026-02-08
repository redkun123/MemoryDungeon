using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public BattleManager battleManager;
    //public event Action<Vector2> OnClick;
    //public event Action OnConfirm;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            battleManager.EndPlayerTurn();
        }
    }
}
