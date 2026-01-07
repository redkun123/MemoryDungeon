using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTurnButton : MonoBehaviour
{
    [SerializeField] private Button _endTurnButton;
    [SerializeField] private BattleManager battleManager;
    public void Awake()
    {
        _endTurnButton.onClick.AddListener(OnClickEndTurn);
    }
    public void OnClickEndTurn()
    {
        battleManager.EndPlayerTurn();
    }
}
