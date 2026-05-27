using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

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
    [SerializeField] public Transform statusArea;
    [SerializeField] public Image enemyAvatar;

    [Header("Attack Animation")]
    [SerializeField] private float attackMoveDistance = 100f;
    [SerializeField] private float attackDuration = 0.12f;
    private Vector2 originalPos;
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
        battleManager.OnEnemyTurnStart += OnEnemyTurnStarted;
        originalPos = avatar.anchoredPosition;
        RegisterUI();
    }
    void RegisterUI()
    {
        //intention.ShowIntention(enemy);
        enemyAvatar = enemy.avatar;
        enemy.OnHPChange += hpBar.Set;
        enemy.OnAttacked += PlayHitEffect;
        enemy.OnAttacking += PlayAttackEffect;
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
    public void PlayAttackEffect(int damage)
    {
        //tam thoi danh manh nhe dung chung anim
        avatar.DOKill();

        Sequence seq = DOTween.Sequence();

        seq.Append(
            avatar.DOAnchorPosX(
                originalPos.x - attackMoveDistance,
                attackDuration
            ).SetEase(Ease.OutQuad)
        );

        seq.Append(
            avatar.DOAnchorPosX(
                originalPos.x,
                attackDuration
            ).SetEase(Ease.InQuad)
        );
    }
}

