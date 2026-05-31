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
    private void Awake()
    {
        player = RunManager.Instance.player;
        enemy = RunManager.Instance.enemy;
    }
    public IEnumerator ExecuteEffect(IEffectExecute effect, Character source, Character target)
    {
        var ctx = new EffectContext(source, target, effect.GetValue(), effect.GetEffectType());
        source.statusManager.ApplyModifiers(ctx);
        target.statusManager.ApplyModifiers(ctx);
        Debug.Log("Battle Executor resolve card effect");
        yield return effect.Resolve(ctx);
        source.statusManager.OnAttack(ctx);
        target.statusManager.OnAttacked(ctx);
    }
}
