using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    private Enemy enemy;
    [SerializeField] public EnemyIntention intention;
    [SerializeField] public HPBar hpBar;
    [SerializeField] private HPCount hpCount;
    //[SerializeField] public TextMeshProUGUI intentionText;

    public void Bind(Enemy enemy, BattleManager battleManager)
    {
        this.enemy = enemy;
        if (intention == null)
        {
            Debug.LogError("EnemyIntention is not assigned in EnemyView prefab");
            return;
        }
        // Init UI trước
        hpBar.InitSet(enemy.currentHP, enemy.maxHP);
        hpCount.Set(enemy.currentHP, enemy.maxHP);

        // Đăng ký event sau
        enemy.OnHPChanged += hpBar.Set;
        enemy.OnHPChanged += hpCount.Set;
        battleManager.OnPlayerTurn += OnPlayerTurnStarted;
        UpdateUI();
    }
    void UpdateUI()
    {
        hpBar.Set(enemy.currentHP, enemy.maxHP);
        hpCount.Set(enemy.currentHP, enemy.maxHP);
        intention.ShowIntention(enemy);
    }
    private void OnPlayerTurnStarted()
    {
        intention.ShowIntention(enemy);
    }   
}

