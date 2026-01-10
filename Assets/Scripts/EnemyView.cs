using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    private Enemy enemy;

    [SerializeField] public HPBar hpBar;
    [SerializeField] private HPCount hpCount;

    public void Bind(Enemy enemy)
    {
        this.enemy = enemy;

        // Init UI trước
        hpBar.InitSet(enemy.currentHP, enemy.maxHP);
        hpCount.Set(enemy.currentHP, enemy.maxHP);

        // Đăng ký event sau
        enemy.OnHPChanged += hpBar.Set;
        enemy.OnHPChanged += hpCount.Set;
        UpdateHP();
    }

    void UpdateHP()
    {
        hpBar.Set(enemy.currentHP, enemy.maxHP);
        hpCount.Set(enemy.currentHP, enemy.maxHP);
    }
}

