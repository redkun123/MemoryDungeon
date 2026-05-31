using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EffectContext
{
    public Character source;
    public Character target;
    public object sourceView;
    public object targetView;
    public int value;
    public EffectType type;
    public EffectContext(Character source, Character target, int value, EffectType type)
    {
        this.source = source;
        this.target = target;
        this.value = value;
        this.type = type;
        BindView();
    }
    private void BindView()
    {
        var player = RunManager.Instance.player;
        var enemy = RunManager.Instance.enemy;
        if (source == player)
        {
            sourceView = RunManager.Instance.battleSceneController.playerView;
        }
        else if (source == enemy)
        {
            sourceView = RunManager.Instance.battleSceneController.enemyView;
        }
        else
        {
            Debug.Log("Source is not a character");
        }
        if (target == player)
        {
            targetView = RunManager.Instance.battleSceneController.playerView;
        }
        else if (target == enemy)
        {
            sourceView = RunManager.Instance.battleSceneController.enemyView;
        }
        else
        {
            Debug.Log("Target is not a character");
        }
    }
}
public enum EffectType
{
    Damage,
    Block,
    LoseHP,
    ApplyStatus,
    Heal,
    Discard,
    Draw
}