using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class EnemyView : MonoBehaviour
{
    private Enemy enemy;
    [SerializeField] public EnemyIntention intention;
    [SerializeField] public HPBar hpBar;
    [SerializeField] private HPCount hpCount;
    [SerializeField] private GuardCount guardCount;
    [SerializeField] private RectTransform avatar;
    [SerializeField] private DamageText damageTextPrefab;
    [SerializeField] private GameObject intentionPanel;

    public void Bind(Enemy enemy, BattleManager battleManager)
    {
        this.enemy = enemy;
        if (intention == null)
        {
            Debug.LogError("EnemyIntention is not assigned in EnemyView prefab");
            return;
        }
        // Init UI trước
        //hpBar.InitSet(enemy.currentHP, enemy.maxHP);
        ////hpCount.Set(enemy.currentHP, enemy.maxHP);

        //// Đăng ký event sau
        //enemy.OnHPChange += hpBar.Set;
        ////enemy.OnHPChange += hpCount.Set;
        battleManager.OnPlayerTurnStart += OnPlayerTurnStarted;
        battleManager.OnEnemyTurn += OnEnemyTurnStarted;
        RegisterUI();
    }
    void RegisterUI()
    {
        //intention.ShowIntention(enemy);
        enemy.OnHPChange += hpBar.Set;
        enemy.OnAttacked += PlayHitEffect;
        hpBar.InitSet(enemy.currentHP, enemy.maxHP);
        enemy.OnModifyGuard += guardCount.ModifyGuard;
        enemy.OnLostGuard += guardCount.LostGuard;
    }
    private void OnPlayerTurnStarted()
    {
        intentionPanel.SetActive(true);
        intention.ShowIntention(enemy);
    }
    private void OnEnemyTurnStarted()
    {
        intentionPanel.SetActive(false);
    }
    public void PlayHitEffect(int damage)
    {
        avatar.DOShakeAnchorPos(
            duration: 0.2f,
            strength: 20f,
            vibrato: 20,
            randomness: 90,
            snapping: false,
            fadeOut: true
        );
        var dmgText = Instantiate(damageTextPrefab, avatar);
        dmgText.Setup(damage);
    }
}

