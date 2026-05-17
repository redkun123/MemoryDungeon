using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private Player player;

    [SerializeField] private HPBar hpBar;
    [SerializeField] private HPCount hpCount;
    [SerializeField] private GuardCount guardCount;
    [SerializeField] public Transform statusArea;
    [SerializeField] private RectTransform avatar;
    [SerializeField] private DamageText damageTextPrefab;

    [Header("Attack Animation")]
    [SerializeField] private float attackMoveDistance = 100f;
    [SerializeField] private float attackDuration = 0.12f;
    private Vector2 originalPos;
    public void Bind(Player player)
    {
        this.player = player;
        originalPos = avatar.anchoredPosition;
        RegisterUI();
    }

    void RegisterUI()
    {
        player.OnHPChange += hpBar.Set;
        player.OnAttacked += PlayHitEffect;
        player.OnAttacking += PlayAttackEffect;
        hpBar.InitSet(player.currentHP, player.maxHP);
        player.OnModifyGuard += guardCount.ModifyGuard;
        player.OnLostGuard += guardCount.LostGuard;
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
        var dmgText = Instantiate( damageTextPrefab,avatar );
        dmgText.Setup(damage);
    }
    public void PlayAttackEffect(int damage)
    {
        //tam thoi danh manh nhe dung chung anim
        avatar.DOKill();
        Sequence seq = DOTween.Sequence();
        seq.Append(
            avatar.DOAnchorPosX(
                originalPos.x + attackMoveDistance,
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
    public void PlayDefendEffect()
    {

    }
    public void PlayInflictEffect()
    {

    }
    public void PlayGainStatusEffect(Status status)
    {

    }
}

