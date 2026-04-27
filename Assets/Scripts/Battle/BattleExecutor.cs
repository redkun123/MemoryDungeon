using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class BattleExecutor
{
    Player player;
    Enemy enemy;
    Character user;
    Character target;
    CardEffect effect;
    public int value;
    //Put on modifier
    //Apply effect len target
    private void Awake()
    {
        player = RunManager.Instance.player;
        enemy = RunManager.Instance.enemy;
    }
    public void ExecuteEffect(IEffectExecute effect, Character source, Character target)
    {
        var ctx = new EffectContext(source, target, effect.GetValue(), effect.GetEffectType());
        source.statusManager.ApplyModifiers(ctx);
        target.statusManager.ApplyModifiers(ctx);
        effect.Resolve(ctx);
        source.statusManager.OnAttack(ctx);
        target.statusManager.OnAttacked(ctx);
    }
    //public void ReceiveEffect(CardEffect effect, Character targetChar)
    //{
    //    this.effect = effect;
    //    target = targetChar;
    //    var targetType = effect.effectTarget;
    //    TargetAcquire(targetType);
    //    //Nhan mo ta effect + target tu card effect
    //    //Phan loai effect xem co can modifier khong
    //}
    //private void TargetAcquire(CardEffect.EffectTarget targetType)
    //{
    //    switch (targetType)
    //    {
    //        case CardEffect.EffectTarget.Self:
    //            user = target;
    //            break;
    //        case CardEffect.EffectTarget.Opponent:
    //            if (target == player)
    //            {
    //                user = enemy;
    //            }
    //            else if (target == enemy)
    //            {
    //                user = player;
    //            }
    //            break;
    //        case CardEffect.EffectTarget.ThisCard:
    //            break;
    //        case CardEffect.EffectTarget.ChooseCard:
    //            break;
    //        default:
    //            Debug.Log("No target!");
    //            break;
    //    }
    //}
}
