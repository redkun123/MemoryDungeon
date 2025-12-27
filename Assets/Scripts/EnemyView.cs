using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : MonoBehaviour
{
    private Enemy enemy;

    [SerializeField] private HPBar hpBar;

    public void Bind(Enemy enemy)
    {
        this.enemy = enemy;
        UpdateHP();
    }

    void UpdateHP()
    {
        enemy.OnHPChanged += hpBar.Set;
        hpBar.Set(enemy.CurrentHP, enemy.MaxHP);
    }
}

