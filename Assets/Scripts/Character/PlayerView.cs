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
    [SerializeField] private RectTransform avatar;
    [SerializeField] private DamageText damageTextPrefab;

    public void Bind(Player player)
    {
        this.player = player;
        RegisterUI();
    }

    void RegisterUI()
    {
        player.OnHPChange += hpBar.Set;
        player.OnAttacked += PlayHitEffect;
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
}

